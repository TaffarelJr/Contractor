using System;
using Contractor;

namespace WebApplication
{
    public class ContractorServiceProvider : IServiceProvider
    {
        private readonly IContainer _container;

        public ContractorServiceProvider(IContainer container)
        {
            _container = container ?? throw new ArgumentNullException(nameof(container));
        }

        public object GetService(Type serviceType)
        {
            var identifier = new Identifier(serviceType);
            return _container.Resolve(identifier);
        }
    }
}
