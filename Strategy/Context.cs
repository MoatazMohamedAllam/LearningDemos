using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy
{
    internal class Context
    {
        private IStrategy _strategy;

        // The Context maintains a reference to one of the Strategy objects. The
        // Context does not know the concrete class of a strategy. It should
        // work with all strategies via the Strategy interface.
        public Context()
        {
            
        }

        // Usually, the Context accepts a strategy through the constructor, but
        // also provides a setter to change it at runtime.
        public Context(IStrategy strategy)
        {
            this._strategy = strategy;
        }

        public void SetStrategy(IStrategy strategy)
        {
            this._strategy = strategy;
        }

        public void DoSomeBusinessLogic()
        {
            Console.WriteLine("Sorting data using The startegy");

            var result = this._strategy.DoAlgorithm(new List<string> { "a", "b", "c", "d", "e", "f", "g", "h", "i" });

            string resultStr = string.Empty;

            foreach (var item in result as List<string>) 
            {
                resultStr += item + ",";
            }
            Console.WriteLine(resultStr);
        }

    }
}
