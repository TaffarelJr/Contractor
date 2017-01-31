using System;
using System.Linq;
using Contractor.Internal;
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
            var factory = new Factory(mockLifetime.Object);
            var subject = new Container();

            // Act
            Action action1 = () => subject.Register(null, null);
            Action action2 = () => subject.Register(identifier, null);
            Action action3 = () => subject.Register(null, factory);

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
            var factory = new Factory(mockLifetime.Object);
            var subject = new Container();

            // Act
            subject.Register(identifier1, factory);
            subject.Register(identifier2, factory);

            // Assert
            subject.Registry.ShouldNotBeNull();
            subject.Registry.Count.ShouldBe(2);

            var registryItem = subject.Registry
                .Where(pair => pair.Key == identifier1)
                .SingleOrDefault();

            registryItem.ShouldNotBeNull();
            registryItem.Value.ShouldBeSameAs(factory);

            registryItem = subject.Registry
                .Where(pair => pair.Key == identifier2)
                .SingleOrDefault();

            registryItem.ShouldNotBeNull();
            registryItem.Value.ShouldBeSameAs(factory);
        }

        [Fact]
        public void Register_ShouldThrowException_WhenTypeIsAlreadyRegistered()
        {
            // Arrange
            var identifier = new Identifier(typeof(string));
            var mockLifetime = new Mock<ILifetime>(MockBehavior.Strict);
            var factory = new Factory(mockLifetime.Object);
            var subject = new Container();

            subject.Register(identifier, factory);

            // Act
            Action action = () => subject.Register(identifier, factory);

            // Assert
            action.ShouldThrow<InvalidOperationException>();
        }

        [Fact]
        public void WhenResolving_ShouldRegisterTypes()
        {
            // Arrange
            var label = "bob";
            var type = typeof(string);
            var subject = new Container();

            // Act
            var genericTuple = subject.WhenResolving(type);
            var labeledTuple = subject.WhenResolving(type, label);

            // Assert
            genericTuple.Container.ShouldBeSameAs(subject);
            genericTuple.Identifier.ShouldNotBeNull();
            genericTuple.Identifier.TypeToResolve.ShouldBeSameAs(type);
            genericTuple.Identifier.Label.ShouldBeNull();

            labeledTuple.Container.ShouldBeSameAs(subject);
            labeledTuple.Identifier.ShouldNotBeNull();
            labeledTuple.Identifier.TypeToResolve.ShouldBeSameAs(type);
            labeledTuple.Identifier.Label.ShouldBe(label);
        }

        [Fact]
        public void WhenResolving_ShouldRegisterGenericTypes()
        {
            // Arrange
            var label = "bob";
            var subject = new Container();

            // Act
            var genericTuple = subject.WhenResolving<string>();
            var labeledTuple = subject.WhenResolving<string>(label);

            // Assert
            genericTuple.Container.ShouldBeSameAs(subject);
            genericTuple.Identifier.ShouldNotBeNull();
            genericTuple.Identifier.TypeToResolve.ShouldBe(typeof(string));
            genericTuple.Identifier.Label.ShouldBeNull();

            labeledTuple.Container.ShouldBeSameAs(subject);
            labeledTuple.Identifier.ShouldNotBeNull();
            labeledTuple.Identifier.TypeToResolve.ShouldBe(typeof(string));
            labeledTuple.Identifier.Label.ShouldBe(label);
        }
    }
}
