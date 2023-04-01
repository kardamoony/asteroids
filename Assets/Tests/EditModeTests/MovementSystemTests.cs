using Asteroids.CoreLayer.Input;
using Asteroids.SimulationLayer.Entities;
using Asteroids.SimulationLayer.GameSystems;
using Asteroids.SimulationLayer.Strategies;
using Moq;
using NUnit.Framework;

public class MovementSystemTests
{
    [Test]
    public void MovementSystem_UpdatesMovables_IfAnyRegistered()
    {
        var modelMock = new Mock<IInputBasedEntityStrategy<IMovable>>();
        modelMock.Setup(m 
            => m.Execute(It.IsAny<IMovable>(), It.IsAny<IInputProvider>(), It.IsAny<float>())).Verifiable();

        var movementSystem = new MovementSystem(modelMock.Object);
        
        movementSystem.Register(Mock.Of<IMovable>(), Mock.Of<IInputProvider>());
        
        movementSystem.FixedUpdate(1f);
        
        modelMock.Verify();
    }
    
    [Test]
    public void MovementSystem_DoesNotUpdateMovables_IfNoneRegistered()
    {
        var modelMock = new Mock<IInputBasedEntityStrategy<IMovable>>();
        modelMock.Setup(m
            => m.Execute(It.IsAny<IMovable>(), It.IsAny<IInputProvider>(), It.IsAny<float>())).Verifiable();

        var movementSystem = new MovementSystem(modelMock.Object);

        movementSystem.FixedUpdate(1f);
        
        modelMock.Verify(m => m.Execute(It.IsAny<IMovable>(), It.IsAny<IInputProvider>(), It.IsAny<float>()), Times.Never);
    }
    
    [Test]
    public void MovementSystem_DoesNotUpdateMovables_IfAllUnregistered()
    {
        var modelMock = new Mock<IInputBasedEntityStrategy<IMovable>>();
        modelMock.Setup(m
            => m.Execute(It.IsAny<IMovable>(), It.IsAny<IInputProvider>(), It.IsAny<float>())).Verifiable();

        var movementSystem = new MovementSystem(modelMock.Object);

        var movable = Mock.Of<IMovable>();
        
        movementSystem.Register(movable, Mock.Of<IInputProvider>());
        movementSystem.Unregister(movable);

        movementSystem.FixedUpdate(1f);
        
        modelMock.Verify(m => m.Execute(It.IsAny<IMovable>(), It.IsAny<IInputProvider>(), It.IsAny<float>()), Times.Never);
    }
    
    [Test]
    public void MovementSystem_DoesNotThrow_IfDuplicateIsRegistered()
    {
        var movementSystem = new MovementSystem(Mock.Of<IInputBasedEntityStrategy<IMovable>>());

        var movable = Mock.Of<IMovable>();
        
        movementSystem.Register(movable, Mock.Of<IInputProvider>());
        
        Assert.DoesNotThrow(() => movementSystem.Register(movable, Mock.Of<IInputProvider>()));
    }
    
    [Test]
    public void MovementSystem_DoesNotThrow_IfNotRegisteredIsUnregistered()
    {
        var movementSystem = new MovementSystem(Mock.Of<IInputBasedEntityStrategy<IMovable>>());

        Assert.DoesNotThrow(() => movementSystem.Unregister(Mock.Of<IMovable>()));
    }
}