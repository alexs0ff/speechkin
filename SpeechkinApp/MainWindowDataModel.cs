using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SpeechkinApp.Speech;

namespace SpeechkinApp
{
    public class MainWindowDataModel:INotifyPropertyChanged
    {
        private bool _isStarted;
        private int _currentSource;

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindowDataModel()
        {
            RecognitionItems = new ObservableCollection<RecognitionItem>();
            RecognitionItems.Add(new RecognitionItem{Text = "22"});
            RecognitionItems.Add(new RecognitionItem{Text = "33"});
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

        public int CurrentSource
        {
            get { return _currentSource; }
            set
            {
                _currentSource = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<RecognitionItem> RecognitionItems { get; }
    }
}
