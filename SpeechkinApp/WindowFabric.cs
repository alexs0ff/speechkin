using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Unity;

namespace SpeechkinApp
{
    public class WindowFabric
    {
        public WindowFabric(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        private IUnityContainer _unityContainer;

        public T CreateWindow<T>()
            where T:Window
        {
            return _unityContainer.Resolve<T>();
        }
    }
}
