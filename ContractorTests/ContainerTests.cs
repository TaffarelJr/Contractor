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

        [Fact]
        public void CanResolve_ShouldReturnFalse_WhenIdentifierIsNotInRegistry()
        {
            // Arrange
            var type = typeof(string);
            var identifier1 = new Identifier(typeof(int));
            var identifier2 = new Identifier(type);
            var identifier3 = new Identifier(type, "bob");
            var identifier4 = new Identifier(type, "alice");
            var mockLifetime = new Mock<ILifetime>(MockBehavior.Strict);
            var subject = new Container();

            subject.Register(identifier2, mockLifetime.Object);
            subject.Register(identifier3, mockLifetime.Object);

            // Act
            var result1 = subject.CanResolve(identifier1);
            var result2 = subject.CanResolve(identifier2);
            var result3 = subject.CanResolve(identifier3);
            var result4 = subject.CanResolve(identifier4);

            // Assert
            result1.ShouldBeFalse();
            result2.ShouldBeTrue();
            result3.ShouldBeTrue();
            result4.ShouldBeFalse();
        }

        [Fact]
        public void Resolve_ShouldThrowException_WhenTypeIsNotRegistered()
        {
            // Arrange
            var identifier = new Identifier(typeof(string), "bob");
            var mockLifetime = new Mock<ILifetime>(MockBehavior.Strict);

            var subject = new Container();

            // Act
            Action action = () => subject.Resolve(identifier);

            // Assert
            action.ShouldThrow<InvalidOperationException>();

        }

        [Fact]
        public void Resolve_ShouldReturnSpecifiedInstance()
        {
            // Arrange
            var type = typeof(string);
            var identifier = new Identifier(type, "bob");
            var instance = "That's my name!";

            var mockLifetime = new Mock<ILifetime>(MockBehavior.Strict);
            mockLifetime
                .Setup(l => l.GetInstance())
                .Returns(instance);

            var subject = new Container();
            subject.Register(identifier, mockLifetime.Object);

            // Act
            var result = subject.Resolve(identifier);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<string>();
            result.ShouldBeSameAs(instance);
        }
    }
}
