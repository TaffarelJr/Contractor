using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Contractor.Internal;

namespace Contractor
{
    /// <summary>
    /// An IoC/DI container.
    /// </summary>
    public class Container
    {
        private readonly ConcurrentDictionary<Identifier, Factory> _registry = new ConcurrentDictionary<Identifier, Factory>();

        /// <summary>
        /// Gets the type configurations registered with the container.
        /// </summary>
        public IReadOnlyDictionary<Identifier, Factory> Registry => _registry;

        /// <summary>
        /// Registers a type to be resolved by the container, along with the factory that will be used to resolve it.
        /// </summary>
        /// <param name="identifier">The <see cref="Identifier"/> for the type to be resolved by the container.</param>
        /// <param name="factory">The factory that manages instantiation and dependency injection for the type.</param>
        /// <exception cref="ArgumentNullException"><paramref name="identifier"/> is <b>null</b>.
        /// -or- <paramref name="factory"/> is <b>null</b>.</exception>
        /// <exception cref="InvalidOperationException">The specified identifier is already registered with the container.</exception>
        /// <exception cref="OverflowException">The container already contains the maximum number of registry entries (2,147,483,647).</exception>
        public void Register(Identifier identifier, Factory factory)
        {
            if (identifier == null)
                throw new ArgumentNullException(nameof(identifier));
            if (factory == null)
                throw new ArgumentNullException(nameof(factory));

            if (!_registry.TryAdd(identifier, factory))
            {
                var labelString = identifier.Label == null ? null : $" with label '{identifier.Label}'";
                throw new InvalidOperationException($"A registry entry already exists for type '{identifier.TypeToResolve.FullName}'{labelString}");
            }
        }
    }
}
