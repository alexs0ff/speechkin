using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpeechkinApp.Main;
using SpeechkinApp.Settings;
using SpeechkinApp.Translate;
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
            var proxy = container.Resolve<SettingsProxy>();
            proxy.Load();
            container.RegisterInstance<ISpeechSettings>(proxy);
            container.RegisterInstance<ITranslationSettings>(proxy);

            container.RegisterSingleton<ITranslationApiSender, TranslationApiSender>();

            

            var windowFabric = new WindowFabric(container);
            container.RegisterInstance(windowFabric);

            return container;
        }
    }
}
