using System;

namespace Uniforms.Amplitude
{
    public interface IAmplitude
    {
        /// <summary>
        /// Name to initialize Amplitude instance with. Must be set before
        /// Initialize() call.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Tracks an event.
        /// </summary>
        /// <description>
        /// Events are saved locally. Uploads are batched to occur every 30 events
        /// and every 30 seconds, as well as on app close.
        /// After calling logEvent in your app, you will immediately see data appear
        /// on the Amplitude Website.
        /// It's important to think about what types of events you care about as a developer.
        /// You should aim to track between 50 and 200 types of events within your app.
        /// Common event types are different screens within the app, actions the user
        /// initiates (such as pressing a button), and events you want the user to complete
        /// (such as filling out a form, completing a level, or making a payment).
        /// </description>
        void LogEvent(string eventType, object properties = null, bool outOfSession = false);

        /// <summary>
        /// Initializes the Amplitude with your Amplitude api key and optional user ID.
        /// </summary>
        /// <description>
        void Initialize(string apiKey, string userId = null);
    }
}

