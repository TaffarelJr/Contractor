using System;

namespace Contractor.Factory
{
    /// <summary>
    /// Defines a factory that instantiates objects and injects dependencies in them.
    /// </summary>
    public interface IFactory
    {
        /// <summary>
        /// Gets the <see cref="IContainer"/> to be used to resolve dependencies.
        /// </summary>
        IContainer Container { get; }

        /// <summary>
        /// The type to be constructed.
        /// </summary>
        Type ImplementationType { get; }

        /// <summary>
        /// Instantiates a new instance of <see cref="ImplementationType"/>
        /// and injects any necessary dependencies into it.
        /// </summary>
        /// <returns>A new instance of <see cref="ImplementationType"/>
        /// with any necessary dependencies injected into it.</returns>
        object ConstructNewInstance();
    }
}
