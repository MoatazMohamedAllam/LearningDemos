using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observer
{
    internal class WeatherStation : IObservable
    {
        private List<IObserver> observers = new List<IObserver>();
        private int temperture;
        public void Add(IObserver observer)
        {
            observers.Add(observer);
        }

        public void Notify()
        {
            foreach (var observer in observers)
            {
                observer.Update();
            }
        }

        public void Remove(IObserver observer)
        {
            observers.Remove(observer);
        }
        public int GetTemperture()
        {
            return temperture;
        }
    }
  
}
