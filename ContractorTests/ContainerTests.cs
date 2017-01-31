using System;
using System.Linq;
using Contractor.Internal;
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
            var subject = new Container();

            // Act
            Action action1 = () => subject.Register(null, null);
            Action action2 = () => subject.Register(new Identifier(typeof(string)), null);
            Action action3 = () => subject.Register(null, new Factory());

            // Assert
            action1.ShouldThrow<ArgumentNullException>();
            action2.ShouldThrow<ArgumentNullException>();
            action3.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void Register_ShouldRegisterTypeInfo()
        {
            // Arrange
            var identifier1 = new Identifier(typeof(string), "bob");
            var identifier2 = new Identifier(typeof(string), "alice");
            var factory = new Factory();
            var subject = new Container();

            // Act
            subject.Register(identifier1, factory);
            subject.Register(identifier2, factory);

            // Assert
            subject.Registry.ShouldNotBeNull();
            subject.Registry.Count.ShouldBe(2);

            var registryItem = subject.Registry.First();
            registryItem.Key.ShouldBeSameAs(identifier1);
            registryItem.Value.ShouldBeSameAs(factory);

            registryItem = subject.Registry.Last();
            registryItem.Key.ShouldBeSameAs(identifier2);
            registryItem.Value.ShouldBeSameAs(factory);
        }

        [Fact]
        public void Register_ShouldThrowException_WhenTypeIsAlreadyRegistered()
        {
            // Arrange
            var identifier = new Identifier(typeof(string));
            var factory = new Factory();
            var subject = new Container();

            subject.Register(identifier, factory);

            // Act
            Action action = () => subject.Register(identifier, factory);

            // Assert
            action.ShouldThrow<InvalidOperationException>();
        }
    }
}
