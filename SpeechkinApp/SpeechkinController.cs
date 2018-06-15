using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpeechkinApp.Settings;

namespace SpeechkinApp
{
    public class SpeechkinController: WindowControllerBase<MainWindowDataModel>
    {
        private readonly WindowFabric _windowFabric;

        public SpeechkinController(WindowFabric windowFabric)
        {
            _windowFabric = windowFabric;
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
            
        }
    }
}
