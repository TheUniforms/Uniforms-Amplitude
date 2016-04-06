using System;
using Foundation;

namespace Uniforms.Amplitude.Native.iOS
{
    [BaseType (typeof (NSObject))]
    interface Amplitude
    {
        /// <summary>
        /// Get Amplitude instance.
        /// </summary>
        [Static, Export ("instance")]
        Amplitude Instance();

        /// <summary>
        /// Get Amplitude instance with identifier.
        /// </summary>
        [Static, Export ("instanceWithName:")]
        Amplitude InstanceWithName(string instanceName);

        /// <summary>
        /// Initializes the Amplitude with your Amplitude api key.
        /// </summary>
        /// <description>
        /// We recommend you first initialize your class within your
        /// "didFinishLaunchingWithOptions" method inside your app delegate.
        /// </description>
        [Export ("initializeApiKey:")]
        void InitializeApiKey(string apiKey);

        /// <summary>
        /// Initializes the Amplitude with your Amplitude api key and user ID.
        /// </summary>
        /// <description>
        /// We recommend you first initialize your class within your
        /// "didFinishLaunchingWithOptions" method inside your app delegate.
        /// </description>
        [Export ("initializeApiKey:userId:")]
        void InitializeApiKey(string apiKey, string userId);

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
        [Export ("logEvent:")]
        void LogEvent(string eventType);

        /// <summary>
        /// Tracks an event.
        /// </summary>
        [Export ("logEvent:withEventProperties:")]
        void LogEvent(string eventType, NSDictionary properties);

        /// <summary>
        /// Tracks an event.
        /// </summary>
        [Export ("logEvent:withEventProperties:outOfSession:")]
        void LogEvent(string eventType, NSDictionary properties, bool outOfSession);

        /// <summary>
        /// Tracks revenue.
        /// </summary>
        /// <description>
        /// To track revenue from a user, call LogRevenue() each time the user generates revenue.
        /// Method takes in an NSNumber with the dollar amount of the sale as the only argument.
        /// </description>
        [Export ("logRevenue:")]
        void LogRevenue(NSNumber amount);

        /// <summary>
        /// Tracks revenue with product identifier.
        /// </summary>
        [Export ("logRevenue:quantity:price:")]
        void LogRevenue(string productIdentifier, nint quantity, NSNumber price);

        /// <summary>
        /// Tracks revenue with product identifier and transaction receipt.
        /// </summary>
        [Export ("logRevenue:quantity:price:receipt:")]
        void LogRevenue(string productIdentifier, nint quantity, NSNumber price, NSData receipt);

/*!
 @method
 @abstract
 Update user properties using operations provided via Identify API.
 @param identify                   An AMPIdentify object with the intended user property operations
 @discussion
 To update user properties, first create an AMPIdentify object. For example if you wanted to set a user's gender, and then increment their
 karma count by 1, you would do:
 AMPIdentify *identify = [[[AMPIdentify identify] set:@"gender" value:@"male"] add:@"karma" value:[NSNumber numberWithInt:1]];
 Then you would pass this AMPIdentify object to the identify function to send to the server: [[Amplitude instance] identify:identify];
 The Identify API supports add, set, setOnce, unset operations. See the AMPIdentify.h header file for the method signatures.

- (void)identify:(AMPIdentify *)identify;
 */

        /// <summary>
        /// Adds properties that are tracked on the user level.
        /// </summary>
        /// <description>
        /// An NSDictionary containing any additional data to be tracked.
        /// Property keys must be strings and values must be serializable.
        /// </description>
        [Export ("setUserProperties:")]
        void SetUserProperties(NSDictionary userProperties);

        /// <summary>
        /// Adds or replaces properties that are tracked on the user level.
        /// </summary>
        /// <description>
        /// An NSDictionary containing any additional data to be tracked.
        /// Property keys must be strings and values must be serializable.
        /// </description>
        [Export ("setUserProperties:replace:")]
        void SetUserProperties(NSDictionary userProperties, bool replace);

        /// <summary>
        /// Clears all properties that are tracked on the user level.
        /// </summary>
        [Export ("clearUserProperties")]
        void ClearUserProperties();

        /// <summary>
        /// Manually forces the class to immediately upload all queued events.
        /// </summary>
        /// <description>
        /// Events are saved locally. Uploads are batched to occur every 30 events
        /// and every 30 seconds, as well as on app close. Use this method to force
        /// the class to immediately upload all queued events.
        /// </description>
        [Export ("uploadEvents")]
        void UploadEvents();

        /// <summary>
        /// Gets ot sets the userId.
        /// </summary>
        /// <description>
        /// If your app has its own login system that you want to track users with,
        /// you can set the userId.
        /// </description>
        [Export ("userId")]
        string UserId { [NullAllowed] get; set; }

        /// <summary>
        /// Gets or sets the deviceId.
        /// </summary>
        /// <description>
        /// If your app has its own system for tracking devices, you can set the deviceId.
        /// </description>
        [Export ("deviceId")]
        string DeviceId { get; set; }

        /// <summary>
        /// Gets or sets tracking opt out.
        /// </summary>
        /// <description>
        /// If the user wants to opt out of all tracking, use this method to enable opt
        /// out for them. Once opt out is enabled, no events will be saved locally or
        /// sent to the server. Calling this method again with enabled set to NO will
        /// turn tracking back on for the user.
        /// </description>
        [Export ("optOut")]
        bool OptOut { get; set; }

        /// <summary>
        /// Gets the instance name.
        /// </summary>
        [Export ("instanceName")]
        string InstanceName { get; }

        /// <summary>
        /// Gets the property list path.
        /// </summary>
        [Export ("propertyListPath")]
        string PropertyListPath { get; }

        /// <summary>
        /// Disables sending logged events to Amplitude servers.
        /// </summary>
        /// <description>
        /// If you want to stop logged events from being sent to Amplitude severs,
        /// use this method to set the client to offline. Once offline is enabled,
        /// logged events will not be sent to the server until offline is disabled.
        /// Calling this method again with offline set to NO will allow events to
        /// be sent to server and the client will attempt to send events that have
        /// been queued while offline.
        /// </description>
        [Export ("setOffline:")]
        void SetOffline(bool offline);

        /// <summary>
        /// Enables location tracking.
        /// </summary>
        /// <description>
        /// If the user has granted your app location permissions, the SDK
        /// will also grab the location of the user. Amplitude will never prompt
        /// the user for location permissions itself, this must be done by your app.
        /// </description>
        [Export ("enableLocationListening")]
        void EnableLocationListening();

        /// <summary>
        /// Disables location tracking.
        /// </summary>
        /// <description>
        /// If you want location tracking disabled on startup of the app, call
        /// DisableLocationListening() before you call InitializeApiKey().
        /// </description>
        [Export ("disableLocationListening")]
        void DisableLocationListening();

        /// <summary>
        /// Forces the SDK to update with the user's last known location if possible.
        /// </summary>
        /// <description>
        /// If you want to manually force the SDK to update with the user's last known
        /// location, call updateLocation.
        /// </description>
        [Export ("updateLocation")]
        void UpdateLocation();

        /// <summary>
        /// Uses advertisingIdentifier instead of identifierForVendor as the device ID.
        /// </summary>
        /// <description>
        /// Apple prohibits the use of advertisingIdentifier if your app does not have
        /// advertising. Useful for tying together data from advertising campaigns to
        /// anlaytics data. Must be called before InitializeApiKey() is called.
        /// </description>
        [Export ("useAdvertisingIdForDeviceId")]
        void UseAdvertisingIdForDeviceId();

        /// <summary>
        /// Debugging method to find out how many events are being stored locally on the device.
        /// </summary>
        [Export ("printEventsCount")]
        void PrintEventsCount();
    }
}

