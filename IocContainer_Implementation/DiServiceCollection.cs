namespace IocContainer_Implementation
{
    internal class DiServiceCollection
    {
        private List<ServiceDescriptor> _serviceDescriptors = new List<ServiceDescriptor>();
        public DiServiceCollection()
        {
        }

        internal DiContainer GenerateContainer()
        {
            return new DiContainer(_serviceDescriptors);
        }

        internal void RegisterSingleton<T>()
        {
            _serviceDescriptors.Add(new ServiceDescriptor(typeof(T), ServiceLifeTime.Singleton));
        }

        internal void RegisterSingleton<TService>(TService implementation)
        {
            _serviceDescriptors.Add(new ServiceDescriptor(implementation, ServiceLifeTime.Singleton));
        }

        internal void RegisterSingleton<TService, TImplementation>()
        {
            _serviceDescriptors.Add(new ServiceDescriptor(typeof(TService), typeof(TImplementation), ServiceLifeTime.Singleton));
        }

        internal void RegisterTransient<T>()
        {
            _serviceDescriptors.Add(new ServiceDescriptor(typeof(T), ServiceLifeTime.Transient));
        }

        internal void RegisterTransient<TService, TImplementation>()
        {
            _serviceDescriptors.Add(new ServiceDescriptor(typeof(TService), typeof(TImplementation), ServiceLifeTime.Transient));
        }

        internal void RegisterTransient<TService>(TService implementation)
        {
            _serviceDescriptors.Add(new ServiceDescriptor(implementation, ServiceLifeTime.Transient));
        }
    }
}