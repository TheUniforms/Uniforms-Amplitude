using System;
using Org.Json;
using Newtonsoft.Json.Linq;
using Android.App;

namespace Uniforms.Amplitude.Droid
{
    using NativeImplementation = Com.Amplitude.Api.AmplitudeClient;

    public class Amplitude : IAmplitude
    {
        static Activity _mainActivity;

        /// <summary>
        /// Register platform implementation for using via `Uniforms.Amplitude`.
        /// </summary>
        public static void Register(Activity mainActivity)
        {
            _mainActivity = mainActivity;

            Uniforms.Amplitude.Amplitude.Register(typeof(Amplitude));
        }

        /// <summary>
        /// Amplitude native implementation instance for Android.
        /// </summary>
        public NativeImplementation Native
        {
            get
            {
                if (_native == null)
                {
                    _native = String.IsNullOrEmpty(_name) ?
                        NativeImplementation.Instance :
                        new NativeImplementation();
                }

                return _native;
            }
        }
        NativeImplementation _native;

        #region IAmplitude implementation

        public void Initialize(string apiKey, string userId = null)
        {
            if (userId == null)
            {
                Native.Initialize(_mainActivity, apiKey);
            }
            else
            {
                Native.Initialize(_mainActivity, apiKey, userId);
            }

            Native.EnableForegroundTracking(_mainActivity.Application);
        }

        public void LogEvent(string eventType, object properties = null, bool outOfSession = false)
        {
            _native.LogEvent(eventType, GetProperties(properties), outOfSession);
        }

        public void LogRevenue(double amount)
        {
            _native.LogRevenue(amount);
        }

        public void LogRevenue(string productIdentifier, int quantity, double price, byte[] receipt = null)
        {
            _native.LogRevenue(productIdentifier, quantity, price);

            if (receipt != null)
            {
                Console.WriteLine("Warning! Receipt is not supported yet, sorry.");
            }
        }

        public void SetUserProperties(object userProperties, bool replace = false)
        {
            _native.SetUserProperties(GetProperties(userProperties));
        }

        public void ClearUserProperties()
        {
            _native.ClearUserProperties();
        }

        public void UploadEvents()
        {
            _native.UploadEvents();
        }

        public void EnableLocationListening()
        {
            _native.EnableLocationListening();
        }

        public void DisableLocationListening()
        {
            _native.DisableLocationListening();
        }

        public void UpdateLocation()
        {
            Console.WriteLine("Update location not supported on Android.");
        }

        public void UseAdvertisingIdForDeviceId()
        {
            _native.UseAdvertisingIdForDeviceId();
        }

        public void PrintEventsCount()
        {
            Console.WriteLine("Print events count not supported on Android.");
        }

        /// <summary>
        /// Name to initialize Amplitude instance with. 
        /// </summary>
        /// <description>
        /// Must be set before Initialize() call.
        /// </description>
        public string InstanceName
        {
            get
            {
                return _name ?? "";
            }
            set
            {
                if (_native != null)
                {
                    throw new Exception("Amplitude instance is already initialized!");
                }

                _name = String.IsNullOrEmpty(value) ? null : value;
            }
        }
        string _name;

        /// <summary>
        /// Gets ot sets the userId.
        /// </summary>
        public string UserId
        {
            get { return _native.UserId; }
            set { _native.SetUserId(value); }
        }

        /// <summary>
        /// Gets or sets the deviceId.
        /// </summary>
        public string DeviceId
        {
            get { return _native.DeviceId; }
            set { _native.SetDeviceId(value); }
        }

        /// <summary>
        /// Gets or sets tracking opt out.
        /// </summary>
        public bool OptOut
        {
            get { return _native.IsOptedOut; }
            set { _native.SetOptOut(value); }
        }

        /// <summary>
        /// Disables sending logged events to Amplitude servers.
        /// </summary>
        public bool Offline
        {
            set { _native.SetOffline(value); }
        }

        #endregion

        static JSONObject GetProperties(object properties)
        {
            if (properties == null)
            {
                return null;
            }

            var json = (properties as JObject) != null ?
                (properties as JObject).ToString() :
                Newtonsoft.Json.JsonConvert.SerializeObject(properties);

            return new JSONObject(json);
        }
    }
}

