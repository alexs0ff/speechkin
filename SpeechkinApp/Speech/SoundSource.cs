using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechkinApp.Speech
{
    public abstract class SoundSource:IDisposable
    {
        private ISoundDataDestination _dataDestination;

        public void SetDestination(ISoundDataDestination destination)
        {
            _dataDestination = destination;
        }

        protected void SendData(byte[] data, int count)
        {
            var dest = _dataDestination;

            dest?.OnData(data, count);
        }

        public abstract void Dispose();

        public abstract void Start();

        public abstract void Stop();
    }
}
