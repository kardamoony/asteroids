using Asteroids.CoreLayer.Input;
using Asteroids.SimulationLayer.Entities;
using Asteroids.SimulationLayer.Models;
using Moq;
using NUnit.Framework;
using UnityEngine;

public class RotationTests 
{
    [TestCase(10f, 1f, 0.3f)]
    [TestCase(115f, -1f, 0.74f)]
    public void Rotation_Rotate(float angularSpeed, float input, float deltaTime)
    {
        var rotation = new Rotation();

        var rotatableMock = new Mock<IRotatable>();
        rotatableMock.SetupGet(r => r.AngularSpeed).Returns(angularSpeed);
        rotatableMock.SetupProperty(r => r.RotationAngle, 0f);
        
        var inputMock = new Mock<IInputProvider>();
        inputMock.SetupGet(i => i.HorizontalAxis).Returns(input);
        
        var expectedAngle = Mathf.Abs(input) > float.Epsilon 
            ? input * angularSpeed * deltaTime
            : 0f;
        
        rotation.Rotate(rotatableMock.Object, inputMock.Object, deltaTime);
        
        Assert.AreEqual(expectedAngle, rotatableMock.Object.RotationAngle);
    }
}
