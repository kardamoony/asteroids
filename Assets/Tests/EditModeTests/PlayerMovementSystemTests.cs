using Asteroids.CoreLayer.Interfaces;
using Asteroids.SimulationLayer.GameSystems;
using NUnit.Framework;
using Moq;
using UnityEngine;

public class PlayerMovementSystemTests
{
    /*[TestCase(1f, 10f, 0.25f)]
    [TestCase(-1f, 11f, 0.25f)]
    [TestCase(1f, 0, 0.3f)]
    [TestCase(0f, 100f, 0.3f)]
    public void PlayerMovementSystem_ValidInput(float input, float speed, float deltaTime)
    {
        var inputMock = new Mock<IInputProvider>();
        inputMock.SetupGet(i => i.VerticalAxis).Returns(input);

        var movable = new Mock<IMovable>();
  
        movable.SetupGet(m => m.Speed).Returns(speed);
        movable.SetupProperty(m => m.Translation, Vector3.zero);

        var expected = Vector3.forward * (input * speed * deltaTime);

        var movementSystem = new PlayerMovementSystem(movable.Object, inputMock.Object);
        movementSystem.Update(deltaTime);
        
        Assert.AreEqual(expected, movable.Object.Translation);
    }*/
}
