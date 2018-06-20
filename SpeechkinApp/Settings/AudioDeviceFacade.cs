using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSCore.CoreAudioAPI;

namespace SpeechkinApp.Settings
{
    public class AudioDeviceFacade
    {
        public IEnumerable<AudioDeviceItem> GetDevices(DataFlow flow)
        {
            var devices = MMDeviceEnumerator.EnumerateDevices(flow, DeviceState.Active);

            int index = 0;
            foreach (var device in devices)
            {
                yield return new AudioDeviceItem
                {
                    Id = index,
                    Name = device.FriendlyName
                };

                index++;
            }

        }
    }
}
