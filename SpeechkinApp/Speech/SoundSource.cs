using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechkinApp.Speech
{
    public abstract class SoundSource:IDisposable
    {
        private readonly IList<ISoundDataDestination> _dataDestinations = new List<ISoundDataDestination>();

        public void AddDestination(ISoundDataDestination destination)
        {
            _dataDestinations.Add(destination);
        }

        protected void SendData(byte[] data, int count)
        {
            foreach (var soundDataDestination in _dataDestinations)
            {
                TryInvokeDestination(soundDataDestination, data, count);
            }
        }

        private bool TryInvokeDestination(ISoundDataDestination destination, byte[] data, int count)
        {
            try
            {
                destination?.OnData(data, count);
                return true;
            }
            catch (Exception )
            {

                return false;
            } 
        }

        public abstract void Dispose();

        public abstract void Start();

        public abstract void Stop();
    }
}
