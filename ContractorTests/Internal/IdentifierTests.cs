using System;
using Shouldly;
using Xunit;

namespace Contractor.Internal
{
    public class IdentifierTests
    {
        [Fact]
        public void Constructor_ShouldThrowException_WhenTypeIsNull()
        {
            // Act
            Action action = () => new Identifier(null);

            // Assert
            action.ShouldThrow<ArgumentNullException>();
        }
        [Fact]
        public void Constructor_ShouldStoreType()
        {
            // Arrange
            var type = typeof(string);

            // Act
            var result = new Identifier(type);

            // Assert
            result.ShouldNotBeNull();
            result.TypeToResolve.ShouldBeSameAs(type);
            result.Label.ShouldBeNull();
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData("", null)]
        [InlineData("   ", null)]
        [InlineData("bob", "bob")]
        [InlineData("  bob  ", "bob")]
        public void Constructor_ShouldStoreTypeAndLabel(string givenLabel, string expectedLabel)
        {
            // Arrange
            var type = typeof(string);

            // Act
            var result = new Identifier(type, givenLabel);

            // Assert
            result.ShouldNotBeNull();
            result.TypeToResolve.ShouldBeSameAs(type);
            result.Label.ShouldBe(expectedLabel);
        }

        //[Theory]
        //[InlineData(true, true, true)]
        //[InlineData(false, true, false)]
        //[InlineData(true, false, false)]
        //public void EqualityOperators_ShouldHandleNullValues(bool leftIsNull, bool rightIsNull, bool expectedResult)
        //{
        //    // Arrange
        //    var left = leftIsNull ? null : new Identifier(typeof(int));
        //    var right = rightIsNull ? null : new Identifier(typeof(string));

        //    // Act
        //    var equalityResult = left == right;
        //    var inequalityResult = left != right;

        //    // Assert
        //    equalityResult.ShouldBe(expectedResult);
        //    inequalityResult.ShouldNotBe(expectedResult);
        //}

        //[Theory]
        //[InlineData(typeof(string), null, typeof(string), null, true)]
        //[InlineData(typeof(string), null, typeof(string), "bob", false)]
        //[InlineData(typeof(string), "bob", typeof(string), null, false)]
        //[InlineData(typeof(string), "bob", typeof(string), "bob", true)]
        //[InlineData(typeof(string), null, typeof(int), null, false)]
        //[InlineData(typeof(string), null, typeof(int), "bob", false)]
        //[InlineData(typeof(string), "bob", typeof(int), null, false)]
        //[InlineData(typeof(string), "bob", typeof(int), "bob", false)]
        //[InlineData(typeof(int), null, typeof(string), null, false)]
        //[InlineData(typeof(int), null, typeof(string), "bob", false)]
        //[InlineData(typeof(int), "bob", typeof(string), null, false)]
        //[InlineData(typeof(int), "bob", typeof(string), "bob", false)]
        //public void EqualityOperators_ShouldCompareValues(Type leftType, string leftLabel, Type rightType, string rightLabel, bool expectedResult)
        //{
        //    // Arrange
        //    var left = new Identifier(leftType, leftLabel);
        //    var right = new Identifier(rightType, rightLabel);

        //    // Act
        //    var equalityResult = left == right;
        //    var inequalityResult = left != right;

        //    // Assert
        //    equalityResult.ShouldBe(expectedResult);
        //    inequalityResult.ShouldNotBe(expectedResult);
        //}

        [Fact]
        public void Equals_ShouldReturnFalse_WhenOtherIsWrongType()
        {
            // Arrange
            var subject = new Identifier(typeof(int));

            // Act
            var result = subject.Equals("not an Identifier");

            // Assert
            result.ShouldBeFalse();
        }

        [Fact]
        public void Equals_ShouldReturnFalse_WhenOtherIsNull_AndTypeIsNotKnown()
        {
            // Arrange
            var subject = new Identifier(typeof(int));

            // Act
            var result = subject.Equals((object)null);

            // Assert
            result.ShouldBeFalse();
        }

        [Fact]
        public void Equals_ShouldReturnFalse_WhenOtherIsNull()
        {
            // Arrange
            var subject = new Identifier(typeof(int));

            // Act
            var result = subject.Equals(null);

            // Assert
            result.ShouldBeFalse();
        }

        [Theory]
        [InlineData(typeof(string), null, typeof(string), null, true)]
        [InlineData(typeof(string), null, typeof(string), "bob", false)]
        [InlineData(typeof(string), "bob", typeof(string), null, false)]
        [InlineData(typeof(string), "bob", typeof(string), "bob", true)]
        [InlineData(typeof(string), null, typeof(int), null, false)]
        [InlineData(typeof(string), null, typeof(int), "bob", false)]
        [InlineData(typeof(string), "bob", typeof(int), null, false)]
        [InlineData(typeof(string), "bob", typeof(int), "bob", false)]
        [InlineData(typeof(int), null, typeof(string), null, false)]
        [InlineData(typeof(int), null, typeof(string), "bob", false)]
        [InlineData(typeof(int), "bob", typeof(string), null, false)]
        [InlineData(typeof(int), "bob", typeof(string), "bob", false)]
        public void Equals_ShouldCompareValues(Type subjectType, string subjectLabel, Type otherType, string otherLabel, bool expectedResult)
        {
            // Arrange
            var subject = new Identifier(subjectType, subjectLabel);
            var other = new Identifier(otherType, otherLabel);

            // Act
            var typedResult = subject.Equals(other);
            var untypedResult = subject.Equals((object)other);

            // Assert
            typedResult.ShouldBe(expectedResult);
            untypedResult.ShouldBe(expectedResult);
        }

        public void GetHashCode_ShouldReturnUniqueValues()
        {
            // Arrange
            var subject1 = new Identifier(typeof(string));
            var subject2 = new Identifier(typeof(string), "bob");
            var subject3 = new Identifier(typeof(int));
            var subject4 = new Identifier(typeof(int), "bob");

            // Act
            var result1 = subject1.GetHashCode();
            var result2 = subject2.GetHashCode();
            var result3 = subject3.GetHashCode();
            var result4 = subject4.GetHashCode();

            // Assert
            result1.ShouldNotBe(0);
            result2.ShouldNotBe(0);
            result3.ShouldNotBe(0);
            result4.ShouldNotBe(0);

            result1.ShouldNotBe(result2);
            result1.ShouldNotBe(result3);
            result1.ShouldNotBe(result4);

            result2.ShouldNotBe(result3);
            result2.ShouldNotBe(result4);

            result3.ShouldNotBe(result4);
        }

        [Theory]
        [InlineData(typeof(string), null, "System.String")]
        [InlineData(typeof(string), "bob", "System.String (bob)")]
        public void ToString_ShouldReturnFormattedString(Type type, string label, string expectedResult)
        {
            // Arrange
            var subject = new Identifier(type, label);

            // Act
            var result = subject.ToString();

            // Assert
            result.ShouldBe(expectedResult);
        }
    }
}
