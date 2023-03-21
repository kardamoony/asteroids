using Asteroids.CoreLayer.Input;
using Asteroids.SimulationLayer.Entities;
using Asteroids.SimulationLayer.Models;
using NUnit.Framework;
using Moq;
using UnityEngine;

public class ThrustMovementTests
{
    private const float IdleInput = 0f;
    private const float ForwardInput = 1f;
    private const float BackInput = -1f;


    [TestCase(10f, 0.2f, 0.5f)]
    [TestCase( 12f, 1.2f, 0.89f)] 
    [TestCase( 22f, 12f, 12f)]
    public void ThrustMovement_Acceleration(float speed, float acceleration, float deltaTime)
    {
        var movement = new ThrustMovement(acceleration, 0f, 0f);
        
        var movableMock = new Mock<IMovable>();
        movableMock.SetupGet(m => m.Speed).Returns(speed);
        movableMock.SetupProperty(m => m.Velocity, 0f);
        
        var inputMock = new Mock<IInputProvider>();
        inputMock.SetupGet(i => i.VerticalAxis).Returns(ForwardInput);
        
        var expectedVelocity = Mathf.Min(acceleration * deltaTime, speed);
        
        movement.Move(movableMock.Object, inputMock.Object, deltaTime);
        
        Assert.AreEqual(expectedVelocity, movableMock.Object.Velocity);
    }

    [TestCase(10f, 13f, 0.4f, 0.7f)]
    [TestCase(5f, 7f, 1f, 0.55f)]
    [TestCase(0.1f, 13f, 0.4f, 1.2f)]
    public void ThrustMovement_Deceleration(float velocity, float speed, float deceleration, float deltaTime)
    {
        var movement = new ThrustMovement(0f, deceleration, 0f);
        
        var movableMock = new Mock<IMovable>();
        movableMock.SetupGet(m => m.Speed).Returns(speed);
        movableMock.SetupProperty(m => m.Velocity, velocity);
        
        var inputMock = new Mock<IInputProvider>();
        inputMock.SetupGet(i => i.VerticalAxis).Returns(IdleInput);
        
        var expectedVelocity = Mathf.Max(0f, velocity - deceleration * deltaTime);
        
        movement.Move(movableMock.Object, inputMock.Object, deltaTime);
        
        Assert.AreEqual(expectedVelocity, movableMock.Object.Velocity);
    }

    [TestCase(10f, 13f, 0.4f, 0.7f)]
    [TestCase(5f, 7f, 1f, 0.55f)]
    [TestCase(0.1f, 13f, 0.4f, 1.2f)]
    public void ThrustMovement_Brake(float velocity, float speed, float brake, float deltaTime)
    {
        var movement = new ThrustMovement(0f, 0f, brake);
        
        var movableMock = new Mock<IMovable>();
        movableMock.SetupGet(m => m.Speed).Returns(speed);
        movableMock.SetupProperty(m => m.Velocity, velocity);
        
        var inputMock = new Mock<IInputProvider>();
        inputMock.SetupGet(i => i.VerticalAxis).Returns(BackInput);
        
        var expectedVelocity = Mathf.Max(0f, velocity - brake * deltaTime);
        
        movement.Move(movableMock.Object, inputMock.Object, deltaTime);
        
        Assert.AreEqual(expectedVelocity, movableMock.Object.Velocity);
    }
}
