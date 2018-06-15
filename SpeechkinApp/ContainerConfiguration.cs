using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpeechkinApp.Settings;
using Unity;

namespace SpeechkinApp
{
    public class ContainerConfiguration
    {
        public IUnityContainer GetContainer()
        {
            var container = new UnityContainer();

            container.UseAutoMapper();

            container.RegisterSingleton<SpeechkinController>();
            container.RegisterSingleton<SettingsProxy>();
            container.RegisterInstance<ISpeechSettings>(container.Resolve<SettingsProxy>());

            var windowFabric = new WindowFabric(container);
            container.RegisterInstance(windowFabric);

            return container;
        }
    }
}
