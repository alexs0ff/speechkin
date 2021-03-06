﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechkinApp.Speech
{
    public class SpeechRecognitionClient:IDisposable
    {
        private readonly SystemSource _systemSource;

        private readonly BingSpeechService _bingSpeechService;

        private readonly WaveWriterClient _waveWriterClient;

        private bool _isStarted;

        public SpeechRecognitionClient(SystemSource systemSource, BingSpeechService bingSpeechService, WaveWriterClient waveWriterClient)
        {
            
            _systemSource = systemSource;
            _bingSpeechService = bingSpeechService;
            _waveWriterClient = waveWriterClient;
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
            

            _bingSpeechService.EndRecognition += (sender, args) =>
            {
                Stop();
                par.OnEnd?.Invoke(args.EndReasonText);
            };

            _bingSpeechService.Recognition += (sender, args) =>
            {
                par.OnNewItemAction(args.Item);
            };

            _bingSpeechService.Start();

            _systemSource.AddDestination(_bingSpeechService);
            _systemSource.AddDestination(_waveWriterClient);
            
            _systemSource.Start();
            _waveWriterClient.Start();


        }

        public void Stop()
        {
            if (!_isStarted)
            {
                return;
            }
            _isStarted = false;
            _systemSource?.Stop();
            _bingSpeechService.Stop();
            _waveWriterClient.Stop();
        }

        public void Dispose()
        {
            _isStarted = false;
            _systemSource?.Dispose();
            _bingSpeechService?.Stop();
            _waveWriterClient?.Dispose();
        }
    }
}
