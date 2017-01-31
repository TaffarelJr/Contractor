using System;
using Contractor.Factory;

namespace Contractor.Lifetime
{
    /// <summary>
    /// Defines an object that manages instances created by an <see cref="IFactory"/>.
    /// It instructs the <see cref="IFactory"/> to construct new instances as necessary,
    /// and may cache those instances for future use.
    /// </summary>
    public interface ILifetime
    {
        /// <summary>
        /// The type to be constructed upon request.
        /// </summary>
        Type ImplementationType { get; }

        /// <summary>
        /// Gets or sets the <see cref="IFactory"/> to be used to construct new instances when necessary.
        /// </summary>
        IFactory Factory { get; set; }

        /// <summary>
        /// Instructs the <see cref="IFactory"/> to construct a new instance if necessary, or returns a cached instance.
        /// </summary>
        /// <returns>A new or cached instance of <see cref="ImplementationType"/>.</returns>
        object GetInstance();
    }
}
