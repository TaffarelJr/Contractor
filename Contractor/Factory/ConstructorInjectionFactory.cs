using System;

namespace Contractor.Factory
{
    /// <summary>
    /// Defines a factory that instantiates objects and uses constructorinjects dependencies in them.
    /// </summary>
    public class ConstructorInjectionFactory : BaseFactory
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="container">The <see cref="IContainer"/> to be used to resoleve dependencies.</param>
        /// <param name="implementationType">The type to be constructed.</param>
        /// <exception cref="ArgumentNullException"><paramref name="container"/> is <b>null</b>.
        /// -or- <paramref name="implementationType"/> is <b>null</b>.</exception>
        public ConstructorInjectionFactory(IContainer container, Type implementationType)
            : base(container, implementationType)
        {
        }

        /// <summary>
        /// Instantiates a new instance of <see cref="BaseFactory.ImplementationType"/>
        /// and injects any necessary dependencies into it.
        /// </summary>
        /// <returns>A new instance of <see cref="BaseFactory.ImplementationType"/>
        /// with any necessary dependencies injected into it.</returns>
        public override object ConstructNewInstance()
        {
            throw new NotImplementedException();
        }
    }
}
