﻿namespace IocContainer_Implementation
{
    internal class SomeServiceOne : ISomeService
    {
        private readonly IRandomGuidProvider _randomGuidProvider;
        public SomeServiceOne(IRandomGuidProvider randomGuidProvider)
        {
            _randomGuidProvider = randomGuidProvider;
        }
        public void PrintSomething()
        {
            Console.WriteLine(_randomGuidProvider.RandomGuid);
        }
    }
}