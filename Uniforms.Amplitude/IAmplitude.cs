using System;

namespace Uniforms.Amplitude
{
    public interface IAmplitude
    {
        /// <summary>
        /// Name to initialize Amplitude instance with.
        /// </summary>
        /// <description>
        /// Must be set before calling the Initialize() method or just left empty.
        /// </description>
        string Name { get; set; }

        /// <summary>
        /// Initializes the Amplitude with your Amplitude api key and optional user ID.
        /// </summary>
        void Initialize(string apiKey, string userId = null);

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
        /// Tracks revenue.
        /// </summary>
        /// <description>
        /// To track revenue from a user, call LogRevenue() each time the user generates revenue.
        /// Method takes in a double with the dollar amount of the sale as the only argument.
        /// </description>
        void LogRevenue(double amount);

        /// <summary>
        /// Tracks revenue with product identifier and optional transaction receipt.
        /// </summary>
        void LogRevenue (string productIdentifier, int quantity, double price, byte[] receipt = null);

        /// <summary>
        /// Adds or replaces properties that are tracked on the user level.
        /// </summary>
        void SetUserProperties (object userProperties, bool replace = false);

        /// <summary>
        /// Clears all properties that are tracked on the user level.
        /// </summary>
        void ClearUserProperties ();
    }
}

