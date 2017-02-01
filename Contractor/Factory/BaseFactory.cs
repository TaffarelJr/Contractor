using System;
using System.Reflection;

namespace Contractor.Factory
{
    /// <summary>
    /// Base implementation of <see cref="IFactory"/>, just to encapsulate most of the boilerplate.
    /// </summary>
    public abstract class BaseFactory : IFactory
    {
        private readonly IContainer _container;
        private readonly Type _implementationType;
        private readonly TypeInfo _implementationTypeInfo;

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="container">The <see cref="IContainer"/> to be used to resoleve dependencies.</param>
        /// <param name="implementationType">The type to be constructed.</param>
        /// <exception cref="ArgumentNullException"><paramref name="container"/> is <b>null</b>.
        /// -or- <paramref name="implementationType"/> is <b>null</b>.</exception>
        protected BaseFactory(IContainer container, Type implementationType)
        {
            _container = container ?? throw new ArgumentNullException(nameof(container));
            _implementationType = implementationType ?? throw new ArgumentNullException(nameof(implementationType));
            _implementationTypeInfo = implementationType.GetTypeInfo();
        }

        /// <summary>
        /// Gets the <see cref="IContainer"/> to be used to resolve dependencies.
        /// </summary>
        public IContainer Container => _container;

        /// <summary>
        /// Gets the type to be constructed.
        /// </summary>
        public Type ImplementationType => _implementationType;

        /// <summary>
        /// Gets more detailed information about type to be constructed.
        /// </summary>
        public TypeInfo ImplementationTypeInfo => _implementationTypeInfo;

        /// <summary>
        /// Instantiates a new instance of <see cref="ImplementationType"/>
        /// and injects any necessary dependencies into it.
        /// </summary>
        /// <returns>A new instance of <see cref="ImplementationType"/>
        /// with any necessary dependencies injected into it.</returns>
        public abstract object ConstructNewInstance();
    }
}
