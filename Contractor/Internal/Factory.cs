using System;
using Contractor.Lifetime;

namespace Contractor.Internal
{
    /// <summary>
    /// Groups together the strategies to be used for object instantiation and dependency injection for a single type.
    /// </summary>
    public class Factory
    {
        private readonly ILifetime _lifetime;

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="lifetime">The <see cref="ILifetime"/> object that will handle object instantiation.</param>
        /// <exception cref="ArgumentNullException"><paramref name="lifetime"/> is <b>null</b>.</exception>
        public Factory(ILifetime lifetime)
        {
            _lifetime = lifetime ?? throw new ArgumentNullException(nameof(lifetime));
        }

        /// <summary>
        /// Gets the <see cref="ILifetime"/> object that will handle object instantiation.
        /// </summary>
        public ILifetime Lifetime { get => _lifetime; }
    }
}
