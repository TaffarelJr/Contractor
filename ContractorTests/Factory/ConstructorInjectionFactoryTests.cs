using System;
using Moq;
using Shouldly;
using Xunit;

namespace Contractor.Factory
{
    public class ConstructorInjectionFactoryTests
    {
        [Fact]
        public void Constructor_ShouldValidateParameters()
        {
            // Arrange
            var mockContainer = new Mock<IContainer>(MockBehavior.Strict);
            var type = typeof(string);

            // Act
            Action action1 = () => new ConstructorInjectionFactory(null, null);
            Action action2 = () => new ConstructorInjectionFactory(mockContainer.Object, null);
            Action action3 = () => new ConstructorInjectionFactory(null, type);

            // Assert
            action1.ShouldThrow<ArgumentNullException>();
            action2.ShouldThrow<ArgumentNullException>();
            action3.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void Constructor_ShouldStoreParameters()
        {
            // Arrange
            var mockContainer = new Mock<IContainer>(MockBehavior.Strict);
            var type = typeof(string);

            // Act
            var result = new ConstructorInjectionFactory(mockContainer.Object, type);

            // Assert
            result.Container.ShouldBeSameAs(mockContainer.Object);
            result.ImplementationType.ShouldBeSameAs(type);
        }
    }
}
