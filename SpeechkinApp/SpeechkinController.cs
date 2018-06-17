using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpeechkinApp.Settings;
using SpeechkinApp.Speech;

namespace SpeechkinApp
{
    public class SpeechkinController: WindowControllerBase<MainWindowDataModel>
    {
        private readonly WindowFabric _windowFabric;

        private readonly SpeechRecognitionClient _recognitionClient;

        public SpeechkinController(WindowFabric windowFabric, SpeechRecognitionClient recognitionClient)
        {
            _windowFabric = windowFabric;
            _recognitionClient = recognitionClient;
            Model = new MainWindowDataModel();
            Model.IsStarted = false;
        }

        public void ShowSettings()
        {
            var window = _windowFabric.CreateWindow<SettingsWindow>();

            window.ShowDialog();
        }

        public void StartRecognition()
        {
            _recognitionClient.Start(parameters =>
            {
                parameters.Source = (SourceType) Model.CurrentSource;
            });
            Model.IsStarted = true;
        }

        public void Stop()
        {
            Model.IsStarted = false;
            _recognitionClient.Stop();
        }
    }
}
