using System;
using Contractor.Factory;
using Moq;
using Shouldly;
using Xunit;

namespace Contractor.Lifetime
{
    public class TransientLifetimeTests
    {
        [Fact]
        public void Constructor_ShouldValidateParameters()
        {
            // Arrange
            var type = typeof(string);
            var mockFactory = new Mock<IFactory>(MockBehavior.Strict);

            // Act
            Action action1 = () => new TransientLifetime(null, null);
            Action action2 = () => new TransientLifetime(type, null);
            Action action3 = () => new TransientLifetime(null, mockFactory.Object);

            // Assert
            action1.ShouldThrow<ArgumentNullException>();
            action2.ShouldThrow<ArgumentNullException>();
            action3.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void Constructor_ShouldStoreParameters()
        {
            // Arrange
            var type = typeof(string);
            var mockFactory = new Mock<IFactory>(MockBehavior.Strict);

            // Act
            var result = new TransientLifetime(type, mockFactory.Object);

            // Assert
            result.ImplementationType.ShouldBeSameAs(type);
            result.Factory.ShouldBeSameAs(mockFactory.Object);
        }

        [Fact]
        public void Factory_ShouldThrowException_WhenValueIsNull()
        {
            // Arrange
            var type = typeof(string);
            var mockFactory = new Mock<IFactory>(MockBehavior.Strict);
            var subject = new TransientLifetime(type, mockFactory.Object);

            // Act
            Action action = () => subject.Factory = null;

            // Assert
            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void Factory_ShouldStoreNewValue()
        {
            // Arrange
            var type = typeof(string);
            var mockFactory1 = new Mock<IFactory>(MockBehavior.Strict);
            var mockFactory2 = new Mock<IFactory>(MockBehavior.Strict);
            var subject = new TransientLifetime(type, mockFactory1.Object);

            // Act
            subject.Factory = mockFactory2.Object;

            // Assert
            subject.Factory.ShouldBeSameAs(mockFactory2.Object);
        }

        [Fact]
        public void GetInstance_ShouldConstructNewInstance()
        {
            // Arrange
            var type = typeof(string);
            var instance = "hello";

            var mockFactory = new Mock<IFactory>(MockBehavior.Strict);
            mockFactory
                .Setup(f => f.ConstructNewInstance())
                .Returns(instance);

            var subject = new TransientLifetime(type, mockFactory.Object);

            // Act
            var result = subject.GetInstance();

            // Assert
            result.ShouldBe(instance);
            mockFactory.VerifyAll();
        }
    }
}
