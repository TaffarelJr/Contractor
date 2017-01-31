using System;
using Contractor.Factory;

namespace Contractor.Lifetime
{
    /// <summary>
    /// An <see cref="ILifetime"/> object that creates a single instance and caches it.
    /// </summary>
    /// <remarks><see cref="ContainerLifetime"/>, <see cref="ThreadLifetime"/>, and <see cref="StaticLifetime"/>
    /// are all similar in that they represent singleton lifetimes. The difference between them is their scope:
    /// <list type="bullet">
    ///   <item>
    ///     <description><see cref="ContainerLifetime"/> creates a singleton scoped to a <see cref="Container"/> instance.</description>
    ///   </item>
    ///   <item>
    ///     <description><see cref="ThreadLifetime"/> creates a singleton scoped to a specific thread.</description>
    ///   </item>
    ///   <item>
    ///     <description><see cref="StaticLifetime"/> creates a global singleton that is shared everywhere.</description>
    ///   </item>
    /// </list></remarks>
    public class ContainerLifetime : BaseLifetime
    {
        private object _instance;

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="implementationType">The type to be constructed.</param>
        /// <param name="factory">The <see cref="IFactory"/> to be used to construct the instance.</param>
        /// <exception cref="ArgumentNullException"><paramref name="implementationType"/> is <b>null</b>.
        /// -or- <paramref name="factory"/> is <b>null</b>.</exception>
        public ContainerLifetime(Type implementationType, IFactory factory)
            : base(implementationType, factory)
        {
        }

        /// <summary>
        /// Instructs <see cref="BaseLifetime.Factory"/> to construct a single instance,
        /// and caches it for all future calls.
        /// </summary>
        /// <returns>The singleton instance of <see cref="BaseLifetime.ImplementationType"/>.</returns>
        public override object GetInstance()
        {
            if (_instance == null)
                _instance = Factory.ConstructNewInstance();

            return _instance;
        }
    }
}
