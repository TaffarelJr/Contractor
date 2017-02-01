using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Contractor.Factory
{
    /// <summary>
    /// Defines a factory that instantiates objects and uses constructorinjects dependencies in them.
    /// </summary>
    public class ConstructorInjectionFactory : BaseFactory
    {
        private ConstructorInfo _constructor;
        private Identifier[] _parameters;

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
            if (_constructor == null)
            {
                var candidate = FindSuitableConstructor();

                _constructor = candidate.Constructor;
                _parameters = candidate.Parameters;
            }

            return _constructor.Invoke(ResolveDependencies(_parameters));
        }

        private (ConstructorInfo Constructor, Identifier[] Parameters) FindSuitableConstructor()
        {
            var candidate = ImplementationTypeInfo
                .GetConstructors()
                .Select(c =>
                (
                    Constructor: c,
                    Parameters: c.GetParameters()
                        .Select(p => new Identifier(p.ParameterType))
                        .ToArray()
                ))
                .OrderByDescending(c => c.Parameters.Length)
                .FirstOrDefault(c => c.Parameters
                    .All(p => Container.CanResolve(p)));

            if (candidate.Constructor == null)
                throw new InvalidOperationException($"The type '{ImplementationType.FullName}' does not have all of its dependencies registered in the container.");

            return candidate;
        }

        private object[] ResolveDependencies(IEnumerable<Identifier> parameters)
        {
            return parameters
                .Select(p => Container.Resolve(p))
                .ToArray();
        }
    }
}
