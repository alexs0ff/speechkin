using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SpeechkinApp.Annotations;

namespace SpeechkinApp.Speech
{
    public class RecognitionItem:INotifyPropertyChanged
    {
        private string _text;
        private string _confidence;
        private string _lexicalForm;
        private string _translated;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                OnPropertyChanged();
            }
        }

        public string Confidence
        {
            get { return _confidence; }
            set
            {
                _confidence = value;
                OnPropertyChanged();
            }
        }

        public string LexicalForm
        {
            get { return _lexicalForm; }
            set
            {
                _lexicalForm = value;
                OnPropertyChanged();
            }
        }

        public string Translated
        {
            get { return _translated; }
            set
            {
                _translated = value;
                OnPropertyChanged();
            }
        }


        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
