using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IocContainer_Implementation
{
    internal class ServiceDescriptor
    {
        public Type ServiceType { get; }
        public object Implementation { get; internal set; }
        public Type ImplementationType { get; }

        public ServiceLifeTime Lifetime { get; }

        public ServiceDescriptor(object implementation, ServiceLifeTime lifetime)
        {
            ServiceType = implementation.GetType();
            Implementation = implementation;
            Lifetime = lifetime;
        }

        public ServiceDescriptor(Type type, ServiceLifeTime lifetime)
        {
            ServiceType = type;
            Lifetime = lifetime;
        }

        public ServiceDescriptor(Type servicType, Type implementationType, ServiceLifeTime lifetime)
        {
            ServiceType = servicType;
            ImplementationType = implementationType;
            Lifetime = lifetime;
        }


    }
}
