using System;
using System.Linq;
using Contractor.Lifetime;
using Moq;
using Shouldly;
using Xunit;

namespace Contractor
{
    public class ContainerTests
    {
        [Fact]
        public void Register_ShouldValidateParameters()
        {
            // Arrange
            var identifier = new Identifier(typeof(string));
            var mockLifetime = new Mock<ILifetime>(MockBehavior.Strict);
            var subject = new Container();

            // Act
            Action action1 = () => subject.Register(null, null);
            Action action2 = () => subject.Register(identifier, null);
            Action action3 = () => subject.Register(null, mockLifetime.Object);

            // Assert
            action1.ShouldThrow<ArgumentNullException>();
            action2.ShouldThrow<ArgumentNullException>();
            action3.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void Register_ShouldRegisterTypeInfo()
        {
            // Arrange
            var type = typeof(string);
            var identifier1 = new Identifier(type, "bob");
            var identifier2 = new Identifier(type, "alice");
            var mockLifetime = new Mock<ILifetime>(MockBehavior.Strict);
            var subject = new Container();

            // Act
            subject.Register(identifier1, mockLifetime.Object);
            subject.Register(identifier2, mockLifetime.Object);

            // Assert
            subject.Registry.ShouldNotBeNull();
            subject.Registry.Count.ShouldBe(2);

            var registryItem = subject.Registry
                .Where(pair => pair.Key == identifier1)
                .SingleOrDefault();

            registryItem.ShouldNotBeNull();
            registryItem.Value.ShouldBeSameAs(mockLifetime.Object);

            registryItem = subject.Registry
                .Where(pair => pair.Key == identifier2)
                .SingleOrDefault();

            registryItem.ShouldNotBeNull();
            registryItem.Value.ShouldBeSameAs(mockLifetime.Object);
        }

        [Fact]
        public void Register_ShouldThrowException_WhenTypeIsAlreadyRegistered()
        {
            // Arrange
            var identifier = new Identifier(typeof(string));
            var mockLifetime = new Mock<ILifetime>(MockBehavior.Strict);
            var subject = new Container();

            subject.Register(identifier, mockLifetime.Object);

            // Act
            Action action = () => subject.Register(identifier, mockLifetime.Object);

            // Assert
            action.ShouldThrow<InvalidOperationException>();
        }
    }
}
