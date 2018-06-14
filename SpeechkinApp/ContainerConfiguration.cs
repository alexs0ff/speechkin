using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace SpeechkinApp
{
    public class ContainerConfiguration
    {
        public IUnityContainer GetContainer()
        {
            var container = new UnityContainer();

            container.RegisterSingleton<SpeechkinController>();
            return container;
        }
    }
}
