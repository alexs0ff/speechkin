using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SpeechkinApp
{
    public class WindowControllerBase<T>
    {
        public T Model { get; set; }

        protected Window _window;

        public void SetWindow(Window window)
        {
            window.DataContext = Model;
            _window = window;
        }

        public void Close()
        {
            _window.Close();
        }
    }
}
