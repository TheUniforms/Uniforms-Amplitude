using System;
using NUnit.Framework;
using Uniforms.Amplitude;

namespace AmplTest.iOS
{
    [TestFixture]
    public class AmplitudeTests
    {
        [Test]
        public void Pass()
        {
            Assert.True(true);
        }

        [Test]
        public void LogEvent()
        {
            Amplitude.Instance.LogEvent("Test");
        }
    }
}

