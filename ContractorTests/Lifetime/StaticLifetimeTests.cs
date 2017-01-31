using System;
using Contractor.Factory;
using Moq;
using Shouldly;
using Xunit;

namespace Contractor.Lifetime
{
    public class StaticLifetimeTests
    {
        [Fact]
        public void Constructor_ShouldValidateParameters()
        {
            // Arrange
            var type = typeof(string);
            var mockFactory = new Mock<IFactory>(MockBehavior.Strict);

            // Act
            Action action1 = () => new StaticLifetime(null, null);
            Action action2 = () => new StaticLifetime(type, null);
            Action action3 = () => new StaticLifetime(null, mockFactory.Object);

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
            var result = new StaticLifetime(type, mockFactory.Object);

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
            var subject = new StaticLifetime(type, mockFactory.Object);

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
            var subject = new StaticLifetime(type, mockFactory1.Object);

            // Act
            subject.Factory = mockFactory2.Object;

            // Assert
            subject.Factory.ShouldBeSameAs(mockFactory2.Object);
        }

        [Fact]
        public void GetInstance_ShouldConstructSingleGlobalInstance()
        {
            // Arrange
            var type = typeof(string);
            var instances = new[] { "hello", "goodbye" };
            var count = 0;

            var mockFactory1 = new Mock<IFactory>(MockBehavior.Strict);
            mockFactory1
                .Setup(f => f.ConstructNewInstance())
                .Returns(instances[count++]);

            var mockFactory2 = new Mock<IFactory>(MockBehavior.Strict);

            var subject1 = new StaticLifetime(type, mockFactory1.Object);
            var subject2 = new StaticLifetime(type, mockFactory2.Object);

            // Act
            var result1 = subject1.GetInstance();
            var result2 = subject1.GetInstance();
            var result3 = subject1.GetInstance();

            var result4 = subject2.GetInstance();
            var result5 = subject2.GetInstance();
            var result6 = subject2.GetInstance();

            // Assert
            result1.ShouldBe(instances[0]);
            result2.ShouldBe(instances[0]);
            result3.ShouldBe(instances[0]);

            result4.ShouldBe(instances[0]);
            result5.ShouldBe(instances[0]);
            result6.ShouldBe(instances[0]);

            mockFactory1.VerifyAll();
        }
    }
}
