using System;
using Contractor.Factory;
using Moq;
using Shouldly;
using Xunit;

namespace Contractor.Lifetime
{
    public class SingletonLifetimeTests
    {
        [Fact]
        public void Constructor_ShouldValidateParameters()
        {
            // Arrange
            var type = typeof(string);
            var mockFactory = new Mock<IFactory>(MockBehavior.Strict);

            // Act
            Action action1 = () => new SingletonLifetime(null, null);
            Action action2 = () => new SingletonLifetime(type, null);
            Action action3 = () => new SingletonLifetime(null, mockFactory.Object);

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
            var result = new SingletonLifetime(type, mockFactory.Object);

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
            var subject = new SingletonLifetime(type, mockFactory.Object);

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
            var subject = new SingletonLifetime(type, mockFactory1.Object);

            // Act
            subject.Factory = mockFactory2.Object;

            // Assert
            subject.Factory.ShouldBeSameAs(mockFactory2.Object);
        }

        [Fact]
        public void GetInstance_ShouldConstructSingleInstance()
        {
            // Arrange
            var type = typeof(string);
            var instances = new[] { "hello", "goodbye" };
            var count = 0;

            var mockFactory = new Mock<IFactory>(MockBehavior.Strict);
            mockFactory
                .Setup(f => f.ConstructNewInstance())
                .Returns(instances[count++]);

            var subject = new SingletonLifetime(type, mockFactory.Object);

            // Act
            var result1 = subject.GetInstance();
            var result2 = subject.GetInstance();
            var result3 = subject.GetInstance();

            // Assert
            result1.ShouldBe(instances[0]);
            result2.ShouldBe(instances[0]);
            result3.ShouldBe(instances[0]);

            mockFactory.VerifyAll();
        }
    }
}
