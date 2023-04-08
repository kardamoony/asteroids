using System;
using Asteroids.ServiceLayer.Settings.Converters;
using NUnit.Framework;

public class BoolSettingsConverterTests
{
    [Test]
    public void Converter_CorrectArgumentTrue_Converts()
    {
        var converter = new BoolSettingsConverter();

        var converted = converter.Convert("True");
            
        Assert.IsTrue(converted);
    }
    
    [Test]
    public void Converter_CorrectArgumentFalse_Converts()
    {
        var converter = new BoolSettingsConverter();

        var converted = converter.Convert("False");
            
        Assert.IsFalse(converted);
    }

    [Test]
    public void Converter_IncorrectArgument_Throws()
    {
        var converter = new BoolSettingsConverter();

        Assert.Throws<FormatException>(() => converter.Convert("Howdy"));
    }

}