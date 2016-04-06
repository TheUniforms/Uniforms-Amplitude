using System;
using System.Diagnostics;
using Foundation;
using Newtonsoft.Json.Linq;

[assembly: Xamarin.Forms.Dependency (typeof (Uniforms.Amplitude.Forms.iOS.Amplitude))]

namespace Uniforms.Amplitude.Forms.iOS
{
    using AmplitudeImplementation = Uniforms.Amplitude.iOS.Amplitude;

    public class Amplitude : IAmplitude
    {
        /// <summary>
        /// Empty method just to reference the class in code.
        /// </summary>
        public static void Init() {}

        /// <summary>
        /// Amplitude implementation instance for iOS.
        /// </summary>
        AmplitudeImplementation impl;

        #region IAmplitude implementation

        /// <summary>
        /// Name to initialize Amplitude instance with. Must be set before
        /// Initialize() call.
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (impl != null)
                {
                    throw new Exception("Amplitude instance is already initialized!");
                }

                _name = String.IsNullOrEmpty(value) ? null : value;
            }
        }
        string _name;


        /// <summary>
        /// Tracks an event.
        /// </summary>
        public void LogEvent(string eventType, object properties = null, bool outOfSession = false)
        {
            if (properties == null)
            {
                impl.LogEvent(eventType);
            }
            else
            {
                impl.LogEvent(eventType, GetProperties(properties), outOfSession);
            }
        }

        /// <summary>
        /// Initializes the Amplitude with your Amplitude api key and optional user ID.
        /// </summary>
        public void Initialize(string apiKey, string userId = null)
        {
            if (impl != null)
            {
                throw new Exception("Amplitude instance is already initialized!");
            }

            impl = String.IsNullOrEmpty(Name) ?
                AmplitudeImplementation.Instance() :
                AmplitudeImplementation.InstanceWithName(Name);

            if (String.IsNullOrEmpty(userId))
            {
                impl.InitializeApiKey(apiKey);
            }
            else
            {
                impl.InitializeApiKey(apiKey, userId);
            }
        }

        /// <summary>
        /// Tracks revenue.
        /// </summary>
        public void LogRevenue(double amount)
        {
            impl.LogRevenue(NSNumber.FromDouble(amount));
        }

        /// <summary>
        /// Tracks revenue with product identifier and optional transaction receipt.
        /// </summary>
        public void LogRevenue(string productIdentifier, int quantity, double price, byte[] receipt = null)
        {
            if (receipt != null)
            {
                impl.LogRevenue(productIdentifier, (nint)quantity,
                    NSNumber.FromDouble(price),
                    NSData.FromArray(receipt));
            }
            else
            {
                impl.LogRevenue(productIdentifier, (nint)quantity,
                    NSNumber.FromDouble(price));
            }
        }

        /// <summary>
        /// Adds or replaces properties that are tracked on the user level.
        /// </summary>
        public void SetUserProperties(object userProperties, bool replace = false)
        {
            impl.SetUserProperties(GetProperties(userProperties), replace);
        }

        /// <summary>
        /// Clears all properties that are tracked on the user level.
        /// </summary>
        public void ClearUserProperties()
        {
            impl.ClearUserProperties();
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

