using System;

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
    }
}
