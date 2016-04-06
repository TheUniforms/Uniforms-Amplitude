using System;
using System.Diagnostics;
using Foundation;
using Newtonsoft.Json.Linq;

[assembly: Xamarin.Forms.Dependency (typeof (Uniforms.Amplitude.iOSAmplitude))]

namespace Uniforms.Amplitude.Native.iOS
{
    [Preserve(AllMembers = true)]
    public class FormsAmplitude : IAmplitude
    {
        /// <summary>
        /// Empty method just to reference the class in code.
        /// </summary>
        public static void Init() {}

        /// <summary>
        /// Amplitude implementation instance for iOS.
        /// </summary>
        Amplitude impl;

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
            NSMutableDictionary dict = null;

            if (properties == null)
            {
                impl.LogEvent(eventType);
            }
            else
            {
                var jObject = (properties as JObject) ?? JObject.FromObject(properties);

                dict = new NSMutableDictionary();

                foreach (JProperty p in jObject.Properties())
                {
                    dict.Add((NSString)p.Name, (NSString)p.Value.ToString());
                }

                impl.LogEvent(eventType, dict, outOfSession);
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
                Amplitude.Instance() :
                Amplitude.InstanceWithName(Name);

            if (String.IsNullOrEmpty(userId))
            {
                impl.InitializeApiKey(apiKey);
            }
            else
            {
                impl.InitializeApiKey(apiKey, userId);
            }
        }

        #endregion
    }
}

