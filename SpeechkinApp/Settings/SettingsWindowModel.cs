using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace SpeechkinApp.Settings
{
    public class SettingsWindowModel:INotifyPropertyChanged
    {
        private string _bingSpeechAuthUrl;

        private string _bingSpeechKey1;

        private string _bingSpeechKey2;

        public event PropertyChangedEventHandler PropertyChanged;
        
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string AzureSpeechAuthUrl
        {
            get { return _bingSpeechAuthUrl; }
            set
            {
                _bingSpeechAuthUrl = value;
                OnPropertyChanged();
            }
        }

        public string AzureSpeechPrimaryKey
        {
            get { return _bingSpeechKey1; }
            set
            {
                _bingSpeechKey1 = value;
                OnPropertyChanged();
            }
        }

        public string AzureSpeechSecondaryKey
        {
            get { return _bingSpeechKey2; }
            set
            {
                _bingSpeechKey2 = value;
                OnPropertyChanged();
            }
        }
    }
}
