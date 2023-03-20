using Asteroids.CoreLayer.Interfaces;
using Asteroids.SimulationLayer.GameSystems;
using Moq;
using NUnit.Framework;

public class PlayerRotationSystemTests 
{
    /*[TestCase(1f, 15f, 0.25f)]
    public void PlayerRotationSystem_ValidInput(float input, float angularSpeed, float deltaTime)
    {
        var inputMock = new Mock<IInputProvider>();
        inputMock.SetupGet(i => i.HorizontalAxis).Returns(input);
        
        var rotatableMock = new Mock<IRotatable>();
        rotatableMock.SetupGet(r => r.AngularSpeed).Returns(angularSpeed);
        rotatableMock.SetupProperty(r => r.RotationAngle, 0f);

        var expected = input * angularSpeed * deltaTime;

        var rotationSystem = new PlayerRotationSystem(rotatableMock.Object, inputMock.Object);
        rotationSystem.Update(deltaTime);
        
        Assert.AreEqual(expected, rotatableMock.Object.RotationAngle);
    }*/
}
