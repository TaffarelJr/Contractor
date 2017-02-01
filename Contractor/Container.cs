using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Contractor.Lifetime;

namespace Contractor
{
    /// <summary>
    /// The main implementation of <see cref="IContainer"/>.
    /// </summary>
    public class Container : IContainer
    {
        private readonly ConcurrentDictionary<Identifier, ILifetime> _registry = new ConcurrentDictionary<Identifier, ILifetime>();

        /// <summary>
        /// Gets the type configurations registered with the <see cref="IContainer"/>.
        /// </summary>
        public IReadOnlyDictionary<Identifier, ILifetime> Registry => _registry;

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
        public void Register(Identifier identifier, ILifetime lifetime)
        {
            if (identifier == null)
                throw new ArgumentNullException(nameof(identifier));
            if (lifetime == null)
                throw new ArgumentNullException(nameof(lifetime));

            if (!_registry.TryAdd(identifier, lifetime))
            {
                var labelString = identifier.Label == null ? null : $" with label '{identifier.Label}'";
                throw new InvalidOperationException($"A registry entry already exists for type '{identifier.TypeToResolve.FullName}'{labelString}");
            }
        }

        /// <summary>
        /// Returns a value indicating whether the specified type can be resolved by the <see cref="IContainer"/>.
        /// </summary>
        /// <param name="identifier">The <see cref="Identifier"/> of the type to be resolved by the container.</param>
        /// <returns><b>true</b> if the specified type can be resolved by the <see cref="IContainer"/>; otherwise, <b>false</b>.</returns>
        public bool CanResolve(Identifier identifier)
        {
            return Registry.ContainsKey(identifier);
        }

        /// <summary>
        /// Returns an instance of the specified type from the <see cref="IContainer"/>.
        /// </summary>
        /// <param name="identifier">The <see cref="Identifier"/> of the type to be resolved by the <see cref="IContainer"/>.</param>
        /// <returns>The instance of the specified type, as resolved by the <see cref="IContainer"/>.</returns>
        /// <exception cref="InvalidOperationException">The specified type is not registered in the container.</exception>
        public object Resolve(Identifier identifier)
        {
            if (Registry.TryGetValue(identifier, out ILifetime lifetime))
                return lifetime.GetInstance();

            throw new InvalidOperationException($"The type '{identifier.TypeToResolve.FullName}' is not registered in the container.");
        }
    }
}
