namespace IocContainer_Implementation
{
    internal class DiContainer
    {
        private readonly List<ServiceDescriptor> serviceDescriptors;

        public DiContainer(List<ServiceDescriptor> serviceDescriptors)
        {
            this.serviceDescriptors = serviceDescriptors;
        }

        internal object GetService(Type serviceType)
        {
            var descriptor = serviceDescriptors.SingleOrDefault(x => x.ServiceType == serviceType);
            if (descriptor == null)
            {
                throw new Exception($"Service of type of {serviceType.Name} isn't registered");
            }

            if (descriptor.Implementation != null && descriptor.Lifetime == ServiceLifeTime.Singleton)
            {
                return descriptor.Implementation;
            }

            var actualType = descriptor.ImplementationType ?? descriptor.ServiceType;

            if (actualType.IsAbstract || actualType.IsInterface)
            {
                throw new Exception("Can not instantiate abstract classes or interfaces");
            }

            var constructorInfo = actualType.GetConstructors().First();

            var parameters = constructorInfo.GetParameters()
                .Select(x=>GetService(x.ParameterType)).ToArray();

            var implementation = Activator
                .CreateInstance(actualType, parameters);


            if (descriptor.Lifetime == ServiceLifeTime.Singleton)
            {
                descriptor.Implementation = implementation;
            }

            return implementation;
        }

        internal T GetService<T>()
        {
            return (T)GetService(typeof(T));
        }
    }
}