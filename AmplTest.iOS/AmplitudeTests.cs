using System;
using NUnit.Framework;
using Uniforms.Amplitude;

namespace AmplTest.iOS
{
    [TestFixture]
    public class AmplitudeTests
    {
        [Test]
        public void InstanceName()
        {
            Assert.True(Amplitude.Instance.InstanceName != null);
        }

        [Test]
        public void LogEvent()
        {
            Amplitude.Instance.LogEvent("TestLogEvent");
        }

        [Test]
        public void OfflineMode()
        {
            Amplitude.Instance.Offline = true;

            Amplitude.Instance.Offline = false;
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
            Amplitude.Instance.LogEvent("TestLogEventWithProperties", props);
            Amplitude.Instance.ClearUserProperties();
        }

        [Test]
        public void LogEventWithUserProperties()
        {
            Amplitude.Instance.SetUserProperties(new {
                Role = "Tester"
            });
            Amplitude.Instance.LogEvent("TestLogEventWithUserProperties");
            Amplitude.Instance.ClearUserProperties();
        }

        [Test]
        public void SetAmplitudeProperties()
        {
            var deviceId = Amplitude.Instance.DeviceId;
            var userId = Amplitude.Instance.UserId;

            Amplitude.Instance.UserId = "111111111";
            Amplitude.Instance.DeviceId = "111111111";

            Amplitude.Instance.LogEvent("TestAmplitudeProperties");
            Amplitude.Instance.UploadEvents();

            Amplitude.Instance.DeviceId = deviceId;
            Amplitude.Instance.UserId = userId;
        }

        [Test]
        public void LocationUpdate()
        {
            Amplitude.Instance.EnableLocationListening();
            Amplitude.Instance.UpdateLocation();
            Amplitude.Instance.DisableLocationListening();
        }
    }
}

