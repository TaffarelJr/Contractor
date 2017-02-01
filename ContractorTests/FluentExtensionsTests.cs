using System;
using Contractor.Factory;
using Contractor.Lifetime;
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

        [Fact]
        public void UseLifetime_ShouldValidateParameters()
        {
            // Arrange
            var mockContainer = new Mock<IContainer>(MockBehavior.Strict);
            var identifier = new Identifier(typeof(string));
            var mockLifetime = new Mock<ILifetime>(MockBehavior.Strict);

            // Act
            Action action1 = () => FluentExtensions.UseLifetime((null, null), null);
            Action action2 = () => FluentExtensions.UseLifetime((null, null), mockLifetime.Object);
            Action action3 = () => FluentExtensions.UseLifetime((mockContainer.Object, null), null);
            Action action4 = () => FluentExtensions.UseLifetime((mockContainer.Object, null), mockLifetime.Object);
            Action action5 = () => FluentExtensions.UseLifetime((null, identifier), null);
            Action action6 = () => FluentExtensions.UseLifetime((null, identifier), mockLifetime.Object);

            // Assert
            action1.ShouldThrow<ArgumentNullException>();
            action2.ShouldThrow<ArgumentNullException>();
            action3.ShouldThrow<ArgumentNullException>();
            action4.ShouldThrow<ArgumentNullException>();
            action5.ShouldThrow<ArgumentNullException>();
            action6.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void UseLifetime_ShouldRegisterValues()
        {
            // Arrange
            var identifier = new Identifier(typeof(string));
            var mockLifetime = new Mock<ILifetime>(MockBehavior.Strict);

            var mockContainer = new Mock<IContainer>(MockBehavior.Strict);
            mockContainer
                .Setup(c => c.Register(identifier, mockLifetime.Object));

            // Act
            var result = (mockContainer.Object, identifier).UseLifetime(mockLifetime.Object);

            // Assert
            result.ShouldNotBeNull();
            result.Container.ShouldBeSameAs(mockContainer.Object);
            result.Identifier.ShouldBeSameAs(identifier);

            mockContainer.VerifyAll();
        }

        [Fact]
        public void ReturnNew_ShouldValidateParameters()
        {
            // Arrange
            var mockContainer = new Mock<IContainer>(MockBehavior.Strict);
            var identifier = new Identifier(typeof(string));
            var type = typeof(string);

            // Act
            Action action1 = () => FluentExtensions.ReturnNew((null, null), null);
            Action action2 = () => FluentExtensions.ReturnNew((null, null), type);
            Action action3 = () => FluentExtensions.ReturnNew((mockContainer.Object, null), null);
            Action action4 = () => FluentExtensions.ReturnNew((mockContainer.Object, null), type);
            Action action5 = () => FluentExtensions.ReturnNew((null, identifier), null);
            Action action6 = () => FluentExtensions.ReturnNew((null, identifier), type);

            // Assert
            action1.ShouldThrow<ArgumentNullException>();
            action2.ShouldThrow<ArgumentNullException>();
            action3.ShouldThrow<ArgumentNullException>();
            action4.ShouldThrow<ArgumentNullException>();
            action5.ShouldThrow<ArgumentNullException>();
            action6.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void ReturnNew_ShouldRegisterValues()
        {
            // Arrange
            var identifier = new Identifier(typeof(string));
            var type = typeof(string);

            var mockContainer = new Mock<IContainer>(MockBehavior.Strict);
            mockContainer
                .Setup(c => c.Register(identifier, It.Is<TransientLifetime>(l =>
                       l.Factory is ConstructorInjectionFactory
                    && l.Factory.Container == mockContainer.Object
                    && l.Factory.ImplementationType == type)));

            // Act
            var result = (mockContainer.Object, identifier).ReturnNew(type);

            // Assert
            result.ShouldNotBeNull();
            result.Container.ShouldBeSameAs(mockContainer.Object);
            result.Identifier.ShouldBeSameAs(identifier);

            mockContainer.VerifyAll();
        }

        [Fact]
        public void ReturnNew_ShouldValidateGenericParameters()
        {
            // Arrange
            var mockContainer = new Mock<IContainer>(MockBehavior.Strict);
            var identifier = new Identifier(typeof(string));

            // Act
            Action action1 = () => FluentExtensions.ReturnNew<string>((null, null));
            Action action2 = () => FluentExtensions.ReturnNew<string>((mockContainer.Object, null));
            Action action3 = () => FluentExtensions.ReturnNew<string>((null, identifier));

            // Assert
            action1.ShouldThrow<ArgumentNullException>();
            action2.ShouldThrow<ArgumentNullException>();
            action3.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void ReturnNew_ShouldRegisterGenericValues()
        {
            // Arrange
            var identifier = new Identifier(typeof(string));

            var mockContainer = new Mock<IContainer>(MockBehavior.Strict);
            mockContainer
                .Setup(c => c.Register(identifier, It.Is<TransientLifetime>(l =>
                       l.Factory is ConstructorInjectionFactory
                    && l.Factory.Container == mockContainer.Object
                    && l.Factory.ImplementationType == typeof(string))));

            // Act
            var result = (mockContainer.Object, identifier).ReturnNew<string>();

            // Assert
            result.ShouldNotBeNull();
            result.Container.ShouldBeSameAs(mockContainer.Object);
            result.Identifier.ShouldBeSameAs(identifier);

            mockContainer.VerifyAll();
        }

        [Fact]
        public void ReturnSingleton_ShouldValidateParameters()
        {
            // Arrange
            var mockContainer = new Mock<IContainer>(MockBehavior.Strict);
            var identifier = new Identifier(typeof(string));
            var type = typeof(string);

            // Act
            Action action1 = () => FluentExtensions.ReturnSingleton((null, null), null);
            Action action2 = () => FluentExtensions.ReturnSingleton((null, null), type);
            Action action3 = () => FluentExtensions.ReturnSingleton((mockContainer.Object, null), null);
            Action action4 = () => FluentExtensions.ReturnSingleton((mockContainer.Object, null), type);
            Action action5 = () => FluentExtensions.ReturnSingleton((null, identifier), null);
            Action action6 = () => FluentExtensions.ReturnSingleton((null, identifier), type);

            // Assert
            action1.ShouldThrow<ArgumentNullException>();
            action2.ShouldThrow<ArgumentNullException>();
            action3.ShouldThrow<ArgumentNullException>();
            action4.ShouldThrow<ArgumentNullException>();
            action5.ShouldThrow<ArgumentNullException>();
            action6.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void ReturnSingleton_ShouldRegisterValues()
        {
            // Arrange
            var identifier = new Identifier(typeof(string));
            var type = typeof(string);

            var mockContainer = new Mock<IContainer>(MockBehavior.Strict);
            mockContainer
                .Setup(c => c.Register(identifier, It.Is<ContainerLifetime>(l =>
                       l.Factory is ConstructorInjectionFactory
                    && l.Factory.Container == mockContainer.Object
                    && l.Factory.ImplementationType == type)));

            // Act
            var result = (mockContainer.Object, identifier).ReturnSingleton(type);

            // Assert
            result.ShouldNotBeNull();
            result.Container.ShouldBeSameAs(mockContainer.Object);
            result.Identifier.ShouldBeSameAs(identifier);

            mockContainer.VerifyAll();
        }

        [Fact]
        public void ReturnSingleton_ShouldValidateGenericParameters()
        {
            // Arrange
            var mockContainer = new Mock<IContainer>(MockBehavior.Strict);
            var identifier = new Identifier(typeof(string));

            // Act
            Action action1 = () => FluentExtensions.ReturnSingleton<string>((null, null));
            Action action2 = () => FluentExtensions.ReturnSingleton<string>((mockContainer.Object, null));
            Action action3 = () => FluentExtensions.ReturnSingleton<string>((null, identifier));

            // Assert
            action1.ShouldThrow<ArgumentNullException>();
            action2.ShouldThrow<ArgumentNullException>();
            action3.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void ReturnSingleton_ShouldRegisterGenericValues()
        {
            // Arrange
            var identifier = new Identifier(typeof(string));

            var mockContainer = new Mock<IContainer>(MockBehavior.Strict);
            mockContainer
                .Setup(c => c.Register(identifier, It.Is<ContainerLifetime>(l =>
                       l.Factory is ConstructorInjectionFactory
                    && l.Factory.Container == mockContainer.Object
                    && l.Factory.ImplementationType == typeof(string))));

            // Act
            var result = (mockContainer.Object, identifier).ReturnSingleton<string>();

            // Assert
            result.ShouldNotBeNull();
            result.Container.ShouldBeSameAs(mockContainer.Object);
            result.Identifier.ShouldBeSameAs(identifier);

            mockContainer.VerifyAll();
        }

        [Fact]
        public void ReturnThreadSingleton_ShouldValidateParameters()
        {
            // Arrange
            var mockContainer = new Mock<IContainer>(MockBehavior.Strict);
            var identifier = new Identifier(typeof(string));
            var type = typeof(string);

            // Act
            Action action1 = () => FluentExtensions.ReturnThreadSingleton((null, null), null);
            Action action2 = () => FluentExtensions.ReturnThreadSingleton((null, null), type);
            Action action3 = () => FluentExtensions.ReturnThreadSingleton((mockContainer.Object, null), null);
            Action action4 = () => FluentExtensions.ReturnThreadSingleton((mockContainer.Object, null), type);
            Action action5 = () => FluentExtensions.ReturnThreadSingleton((null, identifier), null);
            Action action6 = () => FluentExtensions.ReturnThreadSingleton((null, identifier), type);

            // Assert
            action1.ShouldThrow<ArgumentNullException>();
            action2.ShouldThrow<ArgumentNullException>();
            action3.ShouldThrow<ArgumentNullException>();
            action4.ShouldThrow<ArgumentNullException>();
            action5.ShouldThrow<ArgumentNullException>();
            action6.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void ReturnThreadSingleton_ShouldRegisterValues()
        {
            // Arrange
            var identifier = new Identifier(typeof(string));
            var type = typeof(string);

            var mockContainer = new Mock<IContainer>(MockBehavior.Strict);
            mockContainer
                .Setup(c => c.Register(identifier, It.Is<ThreadLifetime>(l =>
                       l.Factory is ConstructorInjectionFactory
                    && l.Factory.Container == mockContainer.Object
                    && l.Factory.ImplementationType == type)));

            // Act
            var result = (mockContainer.Object, identifier).ReturnThreadSingleton(type);

            // Assert
            result.ShouldNotBeNull();
            result.Container.ShouldBeSameAs(mockContainer.Object);
            result.Identifier.ShouldBeSameAs(identifier);

            mockContainer.VerifyAll();
        }

        [Fact]
        public void ReturnThreadSingleton_ShouldValidateGenericParameters()
        {
            // Arrange
            var mockContainer = new Mock<IContainer>(MockBehavior.Strict);
            var identifier = new Identifier(typeof(string));

            // Act
            Action action1 = () => FluentExtensions.ReturnThreadSingleton<string>((null, null));
            Action action2 = () => FluentExtensions.ReturnThreadSingleton<string>((mockContainer.Object, null));
            Action action3 = () => FluentExtensions.ReturnThreadSingleton<string>((null, identifier));

            // Assert
            action1.ShouldThrow<ArgumentNullException>();
            action2.ShouldThrow<ArgumentNullException>();
            action3.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void ReturnThreadSingleton_ShouldRegisterGenericValues()
        {
            // Arrange
            var identifier = new Identifier(typeof(string));

            var mockContainer = new Mock<IContainer>(MockBehavior.Strict);
            mockContainer
                .Setup(c => c.Register(identifier, It.Is<ThreadLifetime>(l =>
                       l.Factory is ConstructorInjectionFactory
                    && l.Factory.Container == mockContainer.Object
                    && l.Factory.ImplementationType == typeof(string))));

            // Act
            var result = (mockContainer.Object, identifier).ReturnThreadSingleton<string>();

            // Assert
            result.ShouldNotBeNull();
            result.Container.ShouldBeSameAs(mockContainer.Object);
            result.Identifier.ShouldBeSameAs(identifier);

            mockContainer.VerifyAll();
        }

        [Fact]
        public void ReturnGlobalSingleton_ShouldValidateParameters()
        {
            // Arrange
            var mockContainer = new Mock<IContainer>(MockBehavior.Strict);
            var identifier = new Identifier(typeof(string));
            var type = typeof(string);

            // Act
            Action action1 = () => FluentExtensions.ReturnGlobalSingleton((null, null), null);
            Action action2 = () => FluentExtensions.ReturnGlobalSingleton((null, null), type);
            Action action3 = () => FluentExtensions.ReturnGlobalSingleton((mockContainer.Object, null), null);
            Action action4 = () => FluentExtensions.ReturnGlobalSingleton((mockContainer.Object, null), type);
            Action action5 = () => FluentExtensions.ReturnGlobalSingleton((null, identifier), null);
            Action action6 = () => FluentExtensions.ReturnGlobalSingleton((null, identifier), type);

            // Assert
            action1.ShouldThrow<ArgumentNullException>();
            action2.ShouldThrow<ArgumentNullException>();
            action3.ShouldThrow<ArgumentNullException>();
            action4.ShouldThrow<ArgumentNullException>();
            action5.ShouldThrow<ArgumentNullException>();
            action6.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void ReturnGlobalSingleton_ShouldRegisterValues()
        {
            // Arrange
            var identifier = new Identifier(typeof(string));
            var type = typeof(string);

            var mockContainer = new Mock<IContainer>(MockBehavior.Strict);
            mockContainer
                .Setup(c => c.Register(identifier, It.Is<StaticLifetime>(l =>
                       l.Factory is ConstructorInjectionFactory
                    && l.Factory.Container == mockContainer.Object
                    && l.Factory.ImplementationType == type)));

            // Act
            var result = (mockContainer.Object, identifier).ReturnGlobalSingleton(type);

            // Assert
            result.ShouldNotBeNull();
            result.Container.ShouldBeSameAs(mockContainer.Object);
            result.Identifier.ShouldBeSameAs(identifier);

            mockContainer.VerifyAll();
        }

        [Fact]
        public void ReturnGlobalSingleton_ShouldValidateGenericParameters()
        {
            // Arrange
            var mockContainer = new Mock<IContainer>(MockBehavior.Strict);
            var identifier = new Identifier(typeof(string));

            // Act
            Action action1 = () => FluentExtensions.ReturnGlobalSingleton<string>((null, null));
            Action action2 = () => FluentExtensions.ReturnGlobalSingleton<string>((mockContainer.Object, null));
            Action action3 = () => FluentExtensions.ReturnGlobalSingleton<string>((null, identifier));

            // Assert
            action1.ShouldThrow<ArgumentNullException>();
            action2.ShouldThrow<ArgumentNullException>();
            action3.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void ReturnGlobalSingleton_ShouldRegisterGenericValues()
        {
            // Arrange
            var identifier = new Identifier(typeof(string));

            var mockContainer = new Mock<IContainer>(MockBehavior.Strict);
            mockContainer
                .Setup(c => c.Register(identifier, It.Is<StaticLifetime>(l =>
                       l.Factory is ConstructorInjectionFactory
                    && l.Factory.Container == mockContainer.Object
                    && l.Factory.ImplementationType == typeof(string))));

            // Act
            var result = (mockContainer.Object, identifier).ReturnGlobalSingleton<string>();

            // Assert
            result.ShouldNotBeNull();
            result.Container.ShouldBeSameAs(mockContainer.Object);
            result.Identifier.ShouldBeSameAs(identifier);

            mockContainer.VerifyAll();
        }
    }
}
