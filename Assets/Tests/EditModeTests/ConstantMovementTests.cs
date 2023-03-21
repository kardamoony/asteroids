using Asteroids.CoreLayer.Input;
using Asteroids.SimulationLayer.Entities;
using Asteroids.SimulationLayer.Models;
using Moq;
using NUnit.Framework;

public class ConstantMovementTests
{
    [TestCase(10f, 0f, 1f, 0.45f)]
    [TestCase(21f, 23f, 01f, 0.45f)]
    public void ConstantMovement_Move(float speed, float velocity, float input, float deltaTime)
    {
        var movement = new ConstantMovement();
        
        var movableMock = new Mock<IMovable>();
        movableMock.SetupGet(m => m.Speed).Returns(speed);
        movableMock.SetupProperty(m => m.Velocity, velocity);
        
        var inputMock = new Mock<IInputProvider>();
        inputMock.SetupGet(i => i.VerticalAxis).Returns(input);

        var expectedVelocity = speed * input;
        
        movement.Move(movableMock.Object, inputMock.Object, deltaTime);
        
        Assert.AreEqual(expectedVelocity, movableMock.Object.Velocity);
    }
        
}