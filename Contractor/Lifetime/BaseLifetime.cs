using System;
using Contractor.Factory;

namespace Contractor.Lifetime
{
    /// <summary>
    /// Base implementation of <see cref="ILifetime"/>, just to encapsulate most of the boilerplate.
    /// </summary>
    public abstract class BaseLifetime : ILifetime
    {
        private readonly Type _implementationType;
        private IFactory _factory;

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="implementationType">The type to be constructed or cached.</param>
        /// <param name="factory">The <see cref="IFactory"/> to be used to construct new instances when necessary.</param>
        /// <exception cref="ArgumentNullException"><paramref name="implementationType"/> is <b>null</b>.
        /// -or- <paramref name="factory"/> is <b>null</b>.</exception>
        protected BaseLifetime(Type implementationType, IFactory factory)
        {
            _implementationType = implementationType ?? throw new ArgumentNullException(nameof(implementationType));
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        /// <summary>
        /// Gets the type to be constructed or cached.
        /// </summary>
        public Type ImplementationType => _implementationType;

        /// <summary>
        /// Gets or sets the <see cref="IFactory"/> to be used to construct new instances when necessary.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <b>null</b>.</exception>
        public IFactory Factory
        {
            get => _factory;
            set => _factory = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Instructs <see cref="Factory"/> to construct a new instance every time, or returns a cached instance.
        /// </summary>
        /// <returns>A new or cached instance of <see cref="ImplementationType"/>.</returns>
        public abstract object GetInstance();
    }
}
