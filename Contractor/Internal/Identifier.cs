using System;

namespace Contractor.Internal
{
    /// <summary>
    /// Identifies a type to be resolved by the container.
    /// </summary>
    public class Identifier
    {
        private readonly Type _typeToResolve;
        private readonly string _label;

        /// <summary>
        /// Initializes a new identifier for the given type.
        /// </summary>
        /// <param name="typeToResolve">The type to be resolved by the container.</param>
        /// <exception cref="ArgumentNullException"><paramref name="typeToResolve"/> is <b>null</b>.</exception>
        public Identifier(Type typeToResolve)
            : this(typeToResolve, null)
        {
        }

        /// <summary>
        /// Initializes a new identifier for the given type and label.
        /// </summary>
        /// <param name="typeToResolve">The type to be resolved by the container.</param>
        /// <param name="label">An optional label that differentiates indentifiers that have the same type to be resolved.</param>
        /// <exception cref="ArgumentNullException"><paramref name="typeToResolve"/> is <b>null</b>.</exception>
        public Identifier(Type typeToResolve, string label)
        {
            _typeToResolve = typeToResolve ?? throw new ArgumentNullException(nameof(typeToResolve));
            _label = GetTrimmedValueOrNull(label);
        }

        /// <summary>
        /// Gets the type to be resolved by the container.
        /// </summary>
        public Type TypeToResolve { get => _typeToResolve; }

        /// <summary>
        /// Gets the optional label for the type to be resolved.
        /// </summary>
        public string Label { get => _label; }

        /// <summary>
        /// Determines whether the specified objects are equal to each other.
        /// </summary>
        /// <param name="left">The first object to be compared.</param>
        /// <param name="right">The second object to be compared.</param>
        /// <returns><b>true</b> if the specified objects are equal to each other; otherwise, <b>false</b>.</returns>
        public static bool operator ==(Identifier left, Identifier right)
        {
            if (ReferenceEquals(left, null))
                return ReferenceEquals(right, null);

            return left.Equals(right);
        }

        /// <summary>
        /// Determines whether the specified objects are not equal to each other.
        /// </summary>
        /// <param name="left">The first object to be compared.</param>
        /// <param name="right">The second object to be compared.</param>
        /// <returns><b>true</b> if the specified objects are equal to each other; otherwise, <b>false</b>.</returns>
        public static bool operator !=(Identifier left, Identifier right)
        {
            if (ReferenceEquals(left, null))
                return !ReferenceEquals(right, null);

            return !left.Equals(right);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns><b>true</b> if the specified object is equal to the current object; otherwise, <b>false</b>.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null))
                return false;

            return Equals(obj as Identifier);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="other">The object to compare with the current object.</param>
        /// <returns><b>true</b> if the specified object is equal to the current object; otherwise, <b>false</b>.</returns>
        public bool Equals(Identifier other)
        {
            if (ReferenceEquals(other, null))
                return false;

            if (!ReferenceEquals(TypeToResolve, other.TypeToResolve))
                return false;

            if (ReferenceEquals(Label, null))
                return ReferenceEquals(other.Label, null);

            return Label.Equals(other.Label, StringComparison.Ordinal);
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = TypeToResolve.GetHashCode();
                hashCode = (hashCode * 397) ^ (Label == null ? 0 : Label.GetHashCode());
                return hashCode;
            }
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            var labelSuffix = Label == null ? null : $" ({Label})";
            return TypeToResolve.FullName + labelSuffix;
        }

        private static string GetTrimmedValueOrNull(string value)
        {
            if (value == null)
                return null;

            value = value.Trim();
            if (value.Length == 0)
                return null;

            return value;
        }
    }
}
