using Asteroids.CoreLayer.Input;
using Asteroids.SimulationLayer.Entities;
using Asteroids.SimulationLayer.GameSystems;
using Asteroids.SimulationLayer.Strategies;
using Moq;
using NUnit.Framework;

public class RotationSystemTests
{
    [Test]
    public void RotationSystem_UpdatesRotation_IfAnyRegistered()
    {
        var modelMock = new Mock<IEntityStrategy<IRotatable>>();
        modelMock.Setup(m 
            => m.Execute(It.IsAny<IRotatable>(), It.IsAny<IInputProvider>(), It.IsAny<float>())).Verifiable();

        var rotationSystem = new RotationSystem(modelMock.Object);
        
        rotationSystem.Register(Mock.Of<IRotatable>(), Mock.Of<IInputProvider>());
        
        rotationSystem.Update(1f);
        
        modelMock.Verify();
    }
    
    [Test]
    public void RotationSystem_DoesNotUpdateRotation_IfNoneRegistered()
    {
        var modelMock = new Mock<IEntityStrategy<IRotatable>>();
        modelMock.Setup(m
            => m.Execute(It.IsAny<IRotatable>(), It.IsAny<IInputProvider>(), It.IsAny<float>())).Verifiable();

        var movementSystem = new RotationSystem(modelMock.Object);

        movementSystem.Update(1f);
        
        modelMock.Verify(m => m.Execute(It.IsAny<IRotatable>(), It.IsAny<IInputProvider>(), It.IsAny<float>()), Times.Never);
    }
    
    [Test]
    public void RotationSystem_DoesNotUpdateRotation_IfAllUnregistered()
    {
        var modelMock = new Mock<IEntityStrategy<IRotatable>>();
        modelMock.Setup(m
            => m.Execute(It.IsAny<IRotatable>(), It.IsAny<IInputProvider>(), It.IsAny<float>())).Verifiable();

        var movementSystem = new RotationSystem(modelMock.Object);

        var rotatable = Mock.Of<IRotatable>();
        
        movementSystem.Register(rotatable, Mock.Of<IInputProvider>());
        movementSystem.Unregister(rotatable);

        movementSystem.Update(1f);
        
        modelMock.Verify(m => m.Execute(It.IsAny<IRotatable>(), It.IsAny<IInputProvider>(), It.IsAny<float>()), Times.Never);
    }
    
    [Test]
    public void RotationSystem_DoesNotThrow_IfDuplicateIsRegistered()
    {
        var rotationSystem = new RotationSystem(Mock.Of<IEntityStrategy<IRotatable>>());

        var rotatable = Mock.Of<IRotatable>();
        
        rotationSystem.Register(rotatable, Mock.Of<IInputProvider>());

        Assert.DoesNotThrow(() => rotationSystem.Register(rotatable, Mock.Of<IInputProvider>()));
    }
    
    [Test]
    public void RotationSystem_MovementSystem_DoesNotThrow_IfNotRegisteredIsUnregistered()
    {
        var rotationSystem = new RotationSystem(Mock.Of<IEntityStrategy<IRotatable>>());

        Assert.DoesNotThrow(() => rotationSystem.Unregister(Mock.Of<IRotatable>()));
    }
        
}