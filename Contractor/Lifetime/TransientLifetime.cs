using System;
using Contractor.Factory;

namespace Contractor.Lifetime
{
    /// <summary>
    /// An <see cref="ILifetime"/> object that creates a new instance every time is it called.
    /// </summary>
    public class TransientLifetime : BaseLifetime
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="implementationType">The type to be constructed.</param>
        /// <param name="factory">The <see cref="IFactory"/> to be used to construct new instances.</param>
        /// <exception cref="ArgumentNullException"><paramref name="implementationType"/> is <b>null</b>.
        /// -or- <paramref name="factory"/> is <b>null</b>.</exception>
        public TransientLifetime(Type implementationType, IFactory factory)
            : base(implementationType, factory)
        {
        }

        /// <summary>
        /// Instructs <see cref="BaseLifetime.Factory"/> to construct a new instance.
        /// </summary>
        /// <returns>A new instance of <see cref="BaseLifetime.ImplementationType"/>.</returns>
        public override object GetInstance()
        {
            return Factory.ConstructNewInstance();
        }
    }
}
