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

        public void LogRevenue(double amount)
        {
            impl.LogRevenue(NSNumber.FromDouble(amount));
        }

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

        public void SetUserProperties(object userProperties, bool replace = false)
        {
            impl.SetUserProperties(GetProperties(userProperties), replace);
        }

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
            var jObject = (properties as JObject) ??
                JObject.FromObject(properties);

            NSError error = null;

            var dict = (NSDictionary)NSJsonSerialization.Deserialize(
                NSData.FromString(jObject.ToString(), NSStringEncoding.UTF8),
                0, out error);

            if (error != null)
            {
                throw new Exception(error.ToString());
            }

            return dict;

            var json = new NSMutableDictionary();

            foreach (JProperty p in jObject.Properties())
            {
                NSObject value = null;

                switch (p.Type)
                {
                    case JTokenType.Integer:
                        value = NSNumber.FromNInt((nint)(int)p.Value);
                        break;
                        
                    case JTokenType.Float:
                        value = NSNumber.FromDouble((double)p.Value);
                        break;

                    default:
                        value = (NSString)p.Value.ToString();
                        break;
                }

                json.Add((NSString)p.Name, value);
            }

            return json;
        }
    }
}

