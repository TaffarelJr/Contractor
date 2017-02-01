using System;
using Contractor.Factory;
using Contractor.Lifetime;

namespace Contractor
{
    /// <summary>
    /// Contains extension methods that add fluent syntax for <see cref="IContainer"/> configuration.
    /// </summary>
    public static class FluentExtensions
    {
        /// <summary>
        /// Creates an <see cref="Identifier"/> for a type to be resolved by an <see cref="IContainer"/>.
        /// </summary>
        /// <typeparam name="T">The type to be resolved by the <see cref="IContainer"/>.</typeparam>
        /// <param name="container">The <see cref="IContainer"/> where the registration will be made.</param>
        /// <returns>A tuple containing the given <see cref="IContainer"/> and the new
        /// <see cref="Identifier"/> that represents the type to be resolved by it.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="container"/> is <b>null</b>.</exception>
        public static (IContainer Container, Identifier Identifier) WhenResolving<T>(this IContainer container)
        {
            return container.WhenResolving(typeof(T), null);
        }

        /// <summary>
        /// Creates an <see cref="Identifier"/> for a type to be resolved by an <see cref="IContainer"/>.
        /// </summary>
        /// <typeparam name="T">The type to be resolved by the <see cref="IContainer"/>.</typeparam>
        /// <param name="container">The <see cref="IContainer"/> where the registration will be made.</param>
        /// <param name="label">An optional label for the type to be resolved by the <see cref="IContainer"/>.</param>
        /// <returns>A tuple containing the given <see cref="IContainer"/> and the new
        /// <see cref="Identifier"/> that represents the type to be resolved by it.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="container"/> is <b>null</b>.</exception>
        public static (IContainer Container, Identifier Identifier) WhenResolving<T>(this IContainer container, string label)
        {
            return container.WhenResolving(typeof(T), label);
        }

        /// <summary>
        /// Creates an <see cref="Identifier"/> for a type to be resolved by an <see cref="IContainer"/>.
        /// </summary>
        /// <param name="container">The <see cref="IContainer"/> where the registration will be made.</param>
        /// <param name="typeToResolve">The type to be resolved by the <see cref="IContainer"/>.</param>
        /// <returns>A tuple containing the given <see cref="IContainer"/> and the new
        /// <see cref="Identifier"/> that represents the type to be resolved by it.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="container"/> is <b>null</b>.
        /// -or- <paramref name="typeToResolve"/> is <b>null</b>.</exception>
        public static (IContainer Container, Identifier Identifier) WhenResolving(this IContainer container, Type typeToResolve)
        {
            return container.WhenResolving(typeToResolve, null);
        }

        /// <summary>
        /// Creates an <see cref="Identifier"/> for a type to be resolved by an <see cref="IContainer"/>.
        /// </summary>
        /// <param name="container">The <see cref="IContainer"/> where the registration will be made.</param>
        /// <param name="typeToResolve">The type to be resolved by the <see cref="IContainer"/>.</param>
        /// <param name="label">An optional label for the type to be resolved by the <see cref="IContainer"/>.</param>
        /// <returns>A tuple containing the given <see cref="IContainer"/> and the new
        /// <see cref="Identifier"/> that represents the type to be resolved by it.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="container"/> is <b>null</b>.
        /// -or- <paramref name="typeToResolve"/> is <b>null</b>.</exception>
        public static (IContainer Container, Identifier Identifier) WhenResolving(this IContainer container, Type typeToResolve, string label)
        {
            return (
                container ?? throw new ArgumentNullException(nameof(container)),
                new Identifier(typeToResolve, label));
        }

        /// <summary>
        /// Registers the given <see cref="Identifier"/> and a new <see cref="TransientLifetime"/>
        /// with the given <see cref="IContainer"/>.
        /// </summary>
        /// <typeparam name="T">The type to be constructed or cached.</typeparam>
        /// <param name="context">A tuple containing the current <see cref="IContainer"/> and the
        /// <see cref="Identifier"/> that will be registered with it.</param>
        /// <returns>The tuple containing the current <see cref="IContainer"/> and the
        /// <see cref="Identifier"/> that was registered with it.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="context.Container"/> is <b>null</b>.
        /// -or- <paramref name="context.Identifier"/> is <b>null</b>.</exception>
        public static (IContainer Container, Identifier Identifier) ReturnNew<T>(
            this (IContainer Container, Identifier Identifier) context)
        {
            return context.ReturnNew(typeof(T));
        }

        /// <summary>
        /// Registers the given <see cref="Identifier"/> and a new <see cref="TransientLifetime"/>
        /// with the given <see cref="IContainer"/>.
        /// </summary>
        /// <param name="context">A tuple containing the current <see cref="IContainer"/> and the
        /// <see cref="Identifier"/> that will be registered with it.</param>
        /// <param name="implementationType">The type to be constructed or cached.</param>
        /// <returns>The tuple containing the current <see cref="IContainer"/> and the
        /// <see cref="Identifier"/> that was registered with it.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="context.Container"/> is <b>null</b>.
        /// -or- <paramref name="context.Identifier"/> is <b>null</b>.
        /// -or- <paramref name="implementationType"/> is <b>null</b>.</exception>
        public static (IContainer Container, Identifier Identifier) ReturnNew(
            this (IContainer Container, Identifier Identifier) context,
            Type implementationType)
        {
            var factory = context.Container.GetDefaultFactoryFor(implementationType);
            var lifetime = new TransientLifetime(implementationType, factory);
            return context.UseLifetime(lifetime);
        }

        /// <summary>
        /// Registers the given <see cref="Identifier"/> and a new <see cref="ContainerLifetime"/>
        /// with the given <see cref="IContainer"/>.
        /// </summary>
        /// <typeparam name="T">The type to be constructed or cached.</typeparam>
        /// <param name="context">A tuple containing the current <see cref="IContainer"/> and the
        /// <see cref="Identifier"/> that will be registered with it.</param>
        /// <returns>The tuple containing the current <see cref="IContainer"/> and the
        /// <see cref="Identifier"/> that was registered with it.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="context.Container"/> is <b>null</b>.
        /// -or- <paramref name="context.Identifier"/> is <b>null</b>.</exception>
        public static (IContainer Container, Identifier Identifier) ReturnSingleton<T>(
            this (IContainer Container, Identifier Identifier) context)
        {
            return context.ReturnSingleton(typeof(T));
        }

        /// <summary>
        /// Registers the given <see cref="Identifier"/> and a new <see cref="ContainerLifetime"/>
        /// with the given <see cref="IContainer"/>.
        /// </summary>
        /// <param name="context">A tuple containing the current <see cref="IContainer"/> and the
        /// <see cref="Identifier"/> that will be registered with it.</param>
        /// <param name="implementationType">The type to be constructed or cached.</param>
        /// <returns>The tuple containing the current <see cref="IContainer"/> and the
        /// <see cref="Identifier"/> that was registered with it.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="context.Container"/> is <b>null</b>.
        /// -or- <paramref name="context.Identifier"/> is <b>null</b>.
        /// -or- <paramref name="implementationType"/> is <b>null</b>.</exception>
        public static (IContainer Container, Identifier Identifier) ReturnSingleton(
            this (IContainer Container, Identifier Identifier) context,
            Type implementationType)
        {
            var factory = context.Container.GetDefaultFactoryFor(implementationType);
            var lifetime = new ContainerLifetime(implementationType, factory);
            return context.UseLifetime(lifetime);
        }

        /// <summary>
        /// Registers the given <see cref="Identifier"/> and a new <see cref="ThreadLifetime"/>
        /// with the given <see cref="IContainer"/>.
        /// </summary>
        /// <typeparam name="T">The type to be constructed or cached.</typeparam>
        /// <param name="context">A tuple containing the current <see cref="IContainer"/> and the
        /// <see cref="Identifier"/> that will be registered with it.</param>
        /// <returns>The tuple containing the current <see cref="IContainer"/> and the
        /// <see cref="Identifier"/> that was registered with it.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="context.Container"/> is <b>null</b>.
        /// -or- <paramref name="context.Identifier"/> is <b>null</b>.</exception>
        public static (IContainer Container, Identifier Identifier) ReturnThreadSingleton<T>(
            this (IContainer Container, Identifier Identifier) context)
        {
            return context.ReturnThreadSingleton(typeof(T));
        }

        /// <summary>
        /// Registers the given <see cref="Identifier"/> and a new <see cref="ThreadLifetime"/>
        /// with the given <see cref="IContainer"/>.
        /// </summary>
        /// <param name="context">A tuple containing the current <see cref="IContainer"/> and the
        /// <see cref="Identifier"/> that will be registered with it.</param>
        /// <param name="implementationType">The type to be constructed or cached.</param>
        /// <returns>The tuple containing the current <see cref="IContainer"/> and the
        /// <see cref="Identifier"/> that was registered with it.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="context.Container"/> is <b>null</b>.
        /// -or- <paramref name="context.Identifier"/> is <b>null</b>.
        /// -or- <paramref name="implementationType"/> is <b>null</b>.</exception>
        public static (IContainer Container, Identifier Identifier) ReturnThreadSingleton(
            this (IContainer Container, Identifier Identifier) context,
            Type implementationType)
        {
            var factory = context.Container.GetDefaultFactoryFor(implementationType);
            var lifetime = new ThreadLifetime(implementationType, factory);
            return context.UseLifetime(lifetime);
        }

        /// <summary>
        /// Registers the given <see cref="Identifier"/> and a new <see cref="StaticLifetime"/>
        /// with the given <see cref="IContainer"/>.
        /// </summary>
        /// <typeparam name="T">The type to be constructed or cached.</typeparam>
        /// <param name="context">A tuple containing the current <see cref="IContainer"/> and the
        /// <see cref="Identifier"/> that will be registered with it.</param>
        /// <returns>The tuple containing the current <see cref="IContainer"/> and the
        /// <see cref="Identifier"/> that was registered with it.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="context.Container"/> is <b>null</b>.
        /// -or- <paramref name="context.Identifier"/> is <b>null</b>.</exception>
        public static (IContainer Container, Identifier Identifier) ReturnGlobalSingleton<T>(
            this (IContainer Container, Identifier Identifier) context)
        {
            return context.ReturnGlobalSingleton(typeof(T));
        }

        /// <summary>
        /// Registers the given <see cref="Identifier"/> and a new <see cref="StaticLifetime"/>
        /// with the given <see cref="IContainer"/>.
        /// </summary>
        /// <param name="context">A tuple containing the current <see cref="IContainer"/> and the
        /// <see cref="Identifier"/> that will be registered with it.</param>
        /// <param name="implementationType">The type to be constructed or cached.</param>
        /// <returns>The tuple containing the current <see cref="IContainer"/> and the
        /// <see cref="Identifier"/> that was registered with it.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="context.Container"/> is <b>null</b>.
        /// -or- <paramref name="context.Identifier"/> is <b>null</b>.
        /// -or- <paramref name="implementationType"/> is <b>null</b>.</exception>
        public static (IContainer Container, Identifier Identifier) ReturnGlobalSingleton(
            this (IContainer Container, Identifier Identifier) context,
            Type implementationType)
        {
            var factory = context.Container.GetDefaultFactoryFor(implementationType);
            var lifetime = new StaticLifetime(implementationType, factory);
            return context.UseLifetime(lifetime);
        }

        /// <summary>
        /// Registers the given <see cref="Identifier"/> and <see cref="ILifetime"/>
        /// with the given <see cref="IContainer"/>.
        /// </summary>
        /// <param name="context">A tuple containing the current <see cref="IContainer"/> and the
        /// <see cref="Identifier"/> that will be registered with it.</param>
        /// <param name="lifetime">The <see cref="ILifetime"/> that will be registered as
        /// the given <see cref="Identifier"/>.</param>
        /// <returns>The tuple containing the current <see cref="IContainer"/> and the
        /// <see cref="Identifier"/> that was registered with it.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="context.Container"/> is <b>null</b>.
        /// -or- <paramref name="context.Identifier"/> is <b>null</b>.
        /// -or- <paramref name="lifetime"/> is <b>null</b>.</exception>
        public static (IContainer Container, Identifier Identifier) UseLifetime(
            this (IContainer Container, Identifier Identifier) context,
            ILifetime lifetime)
        {
            if (context.Container == null)
                throw new ArgumentNullException(nameof(context.Container));
            if (context.Identifier == null)
                throw new ArgumentNullException(nameof(context.Identifier));
            if (lifetime == null)
                throw new ArgumentNullException(nameof(lifetime));

            context.Container.Register(context.Identifier, lifetime);

            return context;
        }

        private static IFactory GetDefaultFactoryFor(this IContainer container, Type implementationType)
        {
            return new ConstructorInjectionFactory(container, implementationType);
        }
    }
}
