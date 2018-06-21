using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SpeechkinApp.Speech;

namespace SpeechkinApp.Main
{
    public class MainWindowDataModel:INotifyPropertyChanged
    {
        private bool _isStarted;
        private int _fromLanguageId;
        private int _toLanguageId;

        public event PropertyChangedEventHandler PropertyChanged;

       

        public MainWindowDataModel()
        {
            RecognitionItems = new ObservableCollection<RecognitionItem>();
            FromLanguages = new ObservableCollection<LanguageItem>();
            ToLanguages = new ObservableCollection<LanguageItem>();
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool IsStarted
        {
            get { return _isStarted; }
            set
            {
                _isStarted = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<RecognitionItem> RecognitionItems { get; }

        public ObservableCollection<LanguageItem> FromLanguages { get; }

        public ObservableCollection<LanguageItem> ToLanguages { get; }

        public int FromLanguageId
        {
            get { return _fromLanguageId; }
            set
            {
                _fromLanguageId = value;
                OnPropertyChanged();
            }
        }

        public int ToLanguageId
        {
            get { return _toLanguageId; }
            set { _toLanguageId = value;
                OnPropertyChanged();}
        }
    }
}
