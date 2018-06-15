using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SpeechkinApp.Settings;
using Unity;

namespace SpeechkinApp
{
    public static class AutoMapperExtention
    {
        public static void UseAutoMapper(this IUnityContainer container)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<SettingsProfile>();
            });

            IMapper mapper = new Mapper(config);

            container.RegisterInstance<IMapper>(mapper);
        }
    }
}
