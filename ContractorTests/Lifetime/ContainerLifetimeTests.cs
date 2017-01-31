using System;
using Contractor.Factory;
using Moq;
using Shouldly;
using Xunit;

namespace Contractor.Lifetime
{
    public class ContainerLifetimeTests
    {
        [Fact]
        public void Constructor_ShouldValidateParameters()
        {
            // Arrange
            var type = typeof(string);
            var mockFactory = new Mock<IFactory>(MockBehavior.Strict);

            // Act
            Action action1 = () => new ContainerLifetime(null, null);
            Action action2 = () => new ContainerLifetime(type, null);
            Action action3 = () => new ContainerLifetime(null, mockFactory.Object);

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
            var result = new ContainerLifetime(type, mockFactory.Object);

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
            var subject = new ContainerLifetime(type, mockFactory.Object);

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
            var subject = new ContainerLifetime(type, mockFactory1.Object);

            // Act
            subject.Factory = mockFactory2.Object;

            // Assert
            subject.Factory.ShouldBeSameAs(mockFactory2.Object);
        }

        [Fact]
        public void GetInstance_ShouldConstructSingleInstancePerLifetime()
        {
            // Arrange
            var type = typeof(string);
            var instances = new[] { "hello", "goodbye" };
            var count1 = 0;
            var count2 = 1;

            var mockFactory1 = new Mock<IFactory>(MockBehavior.Strict);
            mockFactory1
                .Setup(f => f.ConstructNewInstance())
                .Returns(instances[count1++]);

            var mockFactory2 = new Mock<IFactory>(MockBehavior.Strict);
            mockFactory2
                .Setup(f => f.ConstructNewInstance())
                .Returns(instances[count2--]);

            var subject1 = new ContainerLifetime(type, mockFactory1.Object);
            var subject2 = new ContainerLifetime(type, mockFactory2.Object);

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

            result4.ShouldBe(instances[1]);
            result5.ShouldBe(instances[1]);
            result6.ShouldBe(instances[1]);

            mockFactory1.VerifyAll();
            mockFactory2.VerifyAll();
        }
    }
}
