using System;
using Moq;
using Shouldly;
using Xunit;

namespace Contractor
{
    public class FluentExtensionsTests
    {
        [Fact]
        public void WhenResolving_ShouldValidateParameters()
        {
            // Arrange
            var mockContainer = new Mock<IContainer>(MockBehavior.Strict);
            var type = typeof(string);

            // Act
            Action action1 = () => FluentExtensions.WhenResolving(null, null);
            Action action2 = () => FluentExtensions.WhenResolving(mockContainer.Object, null);
            Action action3 = () => FluentExtensions.WhenResolving(null, type);

            // Assert
            action1.ShouldThrow<ArgumentNullException>();
            action2.ShouldThrow<ArgumentNullException>();
            action3.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void WhenResolving_ShouldValidateParameters_WhenLabelIsGiven()
        {
            // Arrange
            var mockContainer = new Mock<IContainer>(MockBehavior.Strict);
            var type = typeof(string);

            // Act
            Action action1 = () => FluentExtensions.WhenResolving(null, null, "hello");
            Action action2 = () => FluentExtensions.WhenResolving(mockContainer.Object, null, "hello");
            Action action3 = () => FluentExtensions.WhenResolving(null, type, "hello");

            // Assert
            action1.ShouldThrow<ArgumentNullException>();
            action2.ShouldThrow<ArgumentNullException>();
            action3.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void WhenResolving_ShouldValidateParameters_WhenGenericTypeIsGiven()
        {
            // Act
            Action action1 = () => FluentExtensions.WhenResolving<string>(null);
            Action action2 = () => FluentExtensions.WhenResolving<string>(null, "hello");

            // Assert
            action1.ShouldThrow<ArgumentNullException>();
            action2.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void WhenResolving_ShouldRegisterTypes()
        {
            // Arrange
            var label = "bob";
            var type = typeof(string);
            var mockContainer = new Mock<IContainer>(MockBehavior.Strict);

            // Act
            var genericTuple = mockContainer.Object.WhenResolving(type);
            var labeledTuple = mockContainer.Object.WhenResolving(type, label);

            // Assert
            genericTuple.Container.ShouldBeSameAs(mockContainer.Object);
            genericTuple.Identifier.ShouldNotBeNull();
            genericTuple.Identifier.TypeToResolve.ShouldBeSameAs(type);
            genericTuple.Identifier.Label.ShouldBeNull();

            labeledTuple.Container.ShouldBeSameAs(mockContainer.Object);
            labeledTuple.Identifier.ShouldNotBeNull();
            labeledTuple.Identifier.TypeToResolve.ShouldBeSameAs(type);
            labeledTuple.Identifier.Label.ShouldBe(label);
        }

        [Fact]
        public void WhenResolving_ShouldRegisterGenericTypes()
        {
            // Arrange
            var label = "bob";
            var mockContainer = new Mock<IContainer>(MockBehavior.Strict);

            // Act
            var genericTuple = mockContainer.Object.WhenResolving<string>();
            var labeledTuple = mockContainer.Object.WhenResolving<string>(label);

            // Assert
            genericTuple.Container.ShouldBeSameAs(mockContainer.Object);
            genericTuple.Identifier.ShouldNotBeNull();
            genericTuple.Identifier.TypeToResolve.ShouldBe(typeof(string));
            genericTuple.Identifier.Label.ShouldBeNull();

            labeledTuple.Container.ShouldBeSameAs(mockContainer.Object);
            labeledTuple.Identifier.ShouldNotBeNull();
            labeledTuple.Identifier.TypeToResolve.ShouldBe(typeof(string));
            labeledTuple.Identifier.Label.ShouldBe(label);
        }
    }
}
