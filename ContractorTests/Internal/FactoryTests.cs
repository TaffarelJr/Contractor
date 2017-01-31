using System;
using Contractor.Lifetime;
using Moq;
using Shouldly;
using Xunit;

namespace Contractor.Internal
{
    public class FactoryTests
    {
        [Fact]
        public void Constructor_ShouldThrowException_WhenLifetimeIsNull()
        {
            // Act
            Action action = () => new Factory(null);

            // Assert
            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void Constructor_ShouldStoreLifetime()
        {
            // Arrange
            var mockLifetime = new Mock<ILifetime>(MockBehavior.Strict);

            // Act
            var result = new Factory(mockLifetime.Object);

            // Assert
            result.Lifetime.ShouldBeSameAs(mockLifetime.Object);
        }
    }
}
