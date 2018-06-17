using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechkinApp.Speech
{
    public class SpeechRecognitionClient:IDisposable
    {
        private readonly DefaultMicrophoneSource _defaultMicrophoneSource;

        private readonly SystemSource _systemSource;

        private readonly BingSpeechService _bingSpeechService;

        private SoundSource _currentSource;

        private bool _isStarted;

        public SpeechRecognitionClient(DefaultMicrophoneSource defaultMicrophoneSource, SystemSource systemSource, BingSpeechService bingSpeechService)
        {
            _defaultMicrophoneSource = defaultMicrophoneSource;
            _systemSource = systemSource;
            _bingSpeechService = bingSpeechService;
        }

        public void Start(Action<RecognitionStartParameters> cfg)
        {
            if (_isStarted)
            {
                throw new InvalidOperationException("Still playing, please stop it");
            }
            var par = new RecognitionStartParameters();

            cfg(par);

            _isStarted = true;

            
            if (par.Source == SourceType.System)
            {
                _currentSource = _systemSource;
            }
            else
            {
                _currentSource = _defaultMicrophoneSource;
            }

            
            _bingSpeechService.Start();

            _currentSource.SetDestination(_bingSpeechService);
            _currentSource.Start();
        }

        public void Stop()
        {
            _currentSource?.Stop();
            _bingSpeechService.Stop();
        }

        public void Dispose()
        {
            _currentSource?.Dispose();
            _bingSpeechService?.Stop();
        }
    }
}
