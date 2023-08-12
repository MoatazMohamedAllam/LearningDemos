using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observer
{
    internal class PhoneDisplay : IObserver
    {
        private WeatherStation station;

        public PhoneDisplay(WeatherStation station)
        {
            this.station = station;
        }
        public void Update()
        {
            // we get temp from Observable here in the observer
            var temp = this.station.GetTemperture();
        }
    }
}
