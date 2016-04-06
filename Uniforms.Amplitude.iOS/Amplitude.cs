using System;
using Foundation;
using Newtonsoft.Json.Linq;

namespace Uniforms.Amplitude.iOS
{
    using NativeImplementation = Uniforms.Amplitude.Native.iOS.Amplitude;

    public class Amplitude : IAmplitude
    {
        /// <summary>
        /// Register platform implementation for using via `Uniforms.Amplitude`.
        /// </summary>
        public static void Register()
        {
            Uniforms.Amplitude.Amplitude.Register(typeof(Amplitude));
        }

        /// <summary>
        /// Amplitude native implementation instance for iOS.
        /// </summary>
        public NativeImplementation Native
        {
            get
            {
                if (_native == null)
                {
                    _native = String.IsNullOrEmpty(_name) ?
                        NativeImplementation.Instance() :
                        NativeImplementation.InstanceWithName(InstanceName);
                }

                return _native;
            }
        }
        NativeImplementation _native;

        #region IAmplitude implementation

        /// <summary>
        /// Name to initialize Amplitude instance with. 
        /// </summary>
        /// <description>
        /// Must be set before Initialize() call. And if name is not set,
        /// trying to get name will initialize default native instance!
        /// So it will always return real native instance name.
        /// </description>
        public string InstanceName
        {
            get
            {
                return Native.InstanceName ?? "";
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
            set { _native.UserId = value; }
        }

        /// <summary>
        /// Gets or sets the deviceId.
        /// </summary>
        public string DeviceId
        {
            get { return _native.DeviceId; }
            set { _native.DeviceId = value; }
        }

        /// <summary>
        /// Gets or sets tracking opt out.
        /// </summary>
        public bool OptOut
        {
            get { return _native.OptOut; }
            set { _native.OptOut = value; }
        }

        /// <summary>
        /// Disables sending logged events to Amplitude servers.
        /// </summary>
        public bool Offline
        {
            set { _native.SetOffline(value); }
        }

        /// <summary>
        /// Tracks an event.
        /// </summary>
        public void LogEvent(string eventType, object properties = null, bool outOfSession = false)
        {
            if (properties == null)
            {
                _native.LogEvent(eventType);
            }
            else
            {
                _native.LogEvent(eventType, GetProperties(properties), outOfSession);
            }
        }

        /// <summary>
        /// Initializes the Amplitude with your Amplitude api key and optional user ID.
        /// </summary>
        public void Initialize(string apiKey, string userId = null)
        {
            if (String.IsNullOrEmpty(userId))
            {
                Native.InitializeApiKey(apiKey);
            }
            else
            {
                Native.InitializeApiKey(apiKey, userId);
            }
        }

        /// <summary>
        /// Tracks revenue.
        /// </summary>
        public void LogRevenue(double amount)
        {
            _native.LogRevenue(NSNumber.FromDouble(amount));
        }

        /// <summary>
        /// Tracks revenue with product identifier and optional transaction receipt.
        /// </summary>
        public void LogRevenue(string productIdentifier, int quantity, double price, byte[] receipt = null)
        {
            if (receipt != null)
            {
                _native.LogRevenue(productIdentifier, (nint)quantity,
                    NSNumber.FromDouble(price),
                    NSData.FromArray(receipt));
            }
            else
            {
                _native.LogRevenue(productIdentifier, (nint)quantity,
                    NSNumber.FromDouble(price));
            }
        }

        /// <summary>
        /// Adds or replaces properties that are tracked on the user level.
        /// </summary>
        public void SetUserProperties(object userProperties, bool replace = false)
        {
            _native.SetUserProperties(GetProperties(userProperties), replace);
        }

        /// <summary>
        /// Clears all properties that are tracked on the user level.
        /// </summary>
        public void ClearUserProperties()
        {
            _native.ClearUserProperties();
        }

        /// <summary>
        /// Manually forces the class to immediately upload all queued events.
        /// </summary>
        public void UploadEvents()
        {
            _native.UploadEvents();
        }

        /// <summary>
        /// Enables location tracking.
        /// </summary>
        public void EnableLocationListening()
        {
            _native.EnableLocationListening();
        }

        /// <summary>
        /// Disables location tracking.
        /// </summary>
        public void DisableLocationListening()
        {
            _native.DisableLocationListening();
        }

        /// <summary>
        /// Forces the SDK to update with the user's last known location if possible.
        /// </summary>
        public void UpdateLocation()
        {
            _native.UpdateLocation();
        }

        /// <summary>
        /// Uses advertisingIdentifier instead of identifierForVendor as the device ID.
        /// </summary>
        public void UseAdvertisingIdForDeviceId()
        {
            _native.UseAdvertisingIdForDeviceId();
        }

        /// <summary>
        /// Debugging method to find out how many events are being stored locally on the device.
        /// </summary>
        public void PrintEventsCount()
        {
            _native.PrintEventsCount();
        }

        #endregion

        //
        // Private methods
        //

        static NSDictionary GetProperties(object properties)
        {
            var json = (properties as JObject) != null ?
                (properties as JObject).ToString() :
                Newtonsoft.Json.JsonConvert.SerializeObject(properties);

            NSError error = null;

            var dict = (NSDictionary)NSJsonSerialization.Deserialize(
                NSData.FromString(json, NSStringEncoding.UTF8),
                0, out error);

            if (error != null)
            {
                throw new Exception(error.ToString());
            }

            return dict;
        }
    }
}

