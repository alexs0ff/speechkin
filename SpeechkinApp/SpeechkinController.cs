using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechkinApp
{
    public class SpeechkinController
    {
        public MainWindowDataModel MainWindowDataModel { get; }

        public SpeechkinController()
        {
            MainWindowDataModel = new MainWindowDataModel();
            MainWindowDataModel.IsStarted = false;
        }

        public void StartRecognition()
        {
            
        }
    }
}
