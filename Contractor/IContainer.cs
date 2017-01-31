using System;
using System.Collections.Generic;
using Contractor.Lifetime;

namespace Contractor
{
    /// <summary>
    /// Defines an IoC/DI container.
    /// </summary>
    public interface IContainer
    {
        /// <summary>
        /// Gets the type configurations registered with the <see cref="IContainer"/>.
        /// </summary>
        IReadOnlyDictionary<Identifier, ILifetime> Registry { get; }

        /// <summary>
        /// Registers an <see cref="Identifier"/> for a type to be resolved by the <see cref="IContainer"/>,
        /// along with the <see cref="ILifetime"/> that will be used to construct it.
        /// </summary>
        /// <param name="identifier">The <see cref="Identifier"/> of the type to be resolved by the container.</param>
        /// <param name="lifetime">The <see cref="ILifetime"/> that manages instances the type.</param>
        /// <exception cref="ArgumentNullException"><paramref name="identifier"/> is <b>null</b>.
        /// -or- <paramref name="lifetime"/> is <b>null</b>.</exception>
        /// <exception cref="InvalidOperationException">The specified identifier is already registered with the container.</exception>
        /// <exception cref="OverflowException">The container already contains the maximum number of registry entries (<see cref="int.MaxValue"/>).</exception>
        void Register(Identifier identifier, ILifetime lifetime);
    }
}
