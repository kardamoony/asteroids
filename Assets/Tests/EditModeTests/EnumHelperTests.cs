using Asteroids.SimulationLayer.Helpers;
using NUnit.Framework;

namespace Tests.EditModeTests
{
    public class EnumHelperTests
    {
        private enum TestEnum
        {
            Test1,
            Test2,
            Test3,
        }
        
        [Test]
        public void EnumHelper_ToFullString()
        {
            var entry = TestEnum.Test2;
            var actual = entry.ToFullString();

            Assert.AreEqual("TestEnum.Test2", actual);
        }
    }
}