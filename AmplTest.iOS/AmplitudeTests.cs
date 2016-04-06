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

        [Test]
        public void LogEventWithProperties()
        {
            Amplitude.Instance.SetUserProperties(new {
                Role = "Tester"
            });

            var props = new {
                Type = "StartChat",
                MaxUsers = 10
            };
            Amplitude.Instance.LogEvent("Test", props);

            Amplitude.Instance.ClearUserProperties();
        }

        [Test]
        public void LogEventWithUserProperties()
        {
            Amplitude.Instance.SetUserProperties(new {
                Role = "Tester"
            });

            Amplitude.Instance.LogEvent("Test");

            Amplitude.Instance.ClearUserProperties();
        }
    }
}

