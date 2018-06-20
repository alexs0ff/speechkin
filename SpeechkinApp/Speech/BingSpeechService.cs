using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.SpeechRecognition;
using SpeechkinApp.Settings;
using SpeechkinApp.Speech.SpeechEventArgs;

namespace SpeechkinApp.Speech
{
    public class BingSpeechService:ISoundDataDestination, IDisposable
    {
        private readonly ISpeechSettings _speechSettings;

        private DataRecognitionClient _client;

        private bool _started;

        public BingSpeechService(ISpeechSettings speechSettings)
        {
            _speechSettings = speechSettings;
        }

        public void OnData(byte[] data, int actualBytes)
        {
            if (!_started)
            {
                return;
            }

            _client.SendAudio(data,actualBytes);
        }

        public void Start()
        {
            _client?.Dispose();

            _client = SpeechRecognitionServiceFactory.CreateDataClient(
                SpeechRecognitionMode.LongDictation,
                _speechSettings.SpeechLanguage,
                _speechSettings.AzureSpeechPrimaryKey/*, _speechSettings.AzureSpeechSecondaryKey,_speechSettings.AzureSpeechAuthUrl*/);

            _started = true;
            ;
            _client.SendAudioFormat(SpeechAudioFormat.create16BitPCMFormat(_speechSettings.SampleRateValue));

            _client.OnResponseReceived+=ClientOnResponseReceived;
            
        }

        private void ClientOnResponseReceived(object sender, SpeechResponseEventArgs speechResponseEventArgs)
        {
            var status = speechResponseEventArgs.PhraseResponse.RecognitionStatus;

            if (status == RecognitionStatus.EndOfDictation ||
                status == RecognitionStatus.DictationEndSilenceTimeout)
            {
                OnEndRecognition(new EndEventArgs{EndReasonText = status.ToString()});
            }
            else
            {
                foreach (var phraseResponseResult in speechResponseEventArgs.PhraseResponse.Results)
                {
                    var item = new RecognitionItem();
                    item.Text = phraseResponseResult.DisplayText;
                    item.Confidence = phraseResponseResult.Confidence.ToString();
                    item.LexicalForm = phraseResponseResult.LexicalForm;
                    OnRecognition(item);
                }
                
            }
        }
        

        public void Stop()
        {
            _client?.EndAudio();

            _client?.Dispose();
            _client = null;
            _started = false;

        }

        public void Dispose()
        {
            _client?.Dispose();
            _started = false;
        }

        public event EventHandler<EndEventArgs> EndRecognition;

        public event EventHandler<RecognizedEventArgs> Recognition;

        protected virtual void OnEndRecognition(EndEventArgs e)
        {
            EndRecognition?.Invoke(this, e);
        }

        protected virtual void OnRecognition(RecognitionItem item)
        {
            Recognition?.Invoke(this, new RecognizedEventArgs(item));
        }
    }
}
