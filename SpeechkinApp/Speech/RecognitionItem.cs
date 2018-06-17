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


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            
        }
    }
}
