# Getting Started with Uniforms.Amplitude

The component provides cross-platform [Amplitude](https://amplitude.com) API bindings for [Xamarin](https://www.xamarin.com)

Plase note, the component developers aren't affiliated with Amplitude!


Quickstart
----------

1. Initialize platform specific classes:

For **iOS** application in `UIApplicationDelegate.FinishedLaunching()`:

```csharp
Uniforms.Amplitude.iOS.Amplitude.Register();
```

For **Android** application in `OnCreate()` of main activity:

```csharp
Uniforms.Amplitude.Droid.Amplitude.Register(this);
```

2. Initialize Amplitude with API key:

```csharp
Amplitude.Instance.Initialize("PASTE YOUR API KEY HERE");
```

3. Send events to Amplitude:

```csharp
using Uniforms.Amplitude;

// ...

Amplitude.Instance.LogEvent("Start");
```


Show me the code
----------------

For **iOS**:

```csharp
// AppDelegate.cs
// ...
using Uniforms.Amplitude;

public override bool FinishedLaunching(UIApplication app, NSDictionary options)
{
    global::Xamarin.Forms.Forms.Init();

    LoadApplication(new App());

    Uniforms.Amplitude.iOS.Amplitude.Register(this);

    // cross-platform interface, this code can be placed
    // to shared or portable application
    Amplitude.Instance.LogEvent("Start");

    return base.FinishedLaunching(app, options);
}
```


For **Android**:

```csharp
// MainActivity.cs
// ...
using Uniforms.Amplitude;

protected override void OnCreate(Bundle bundle)
{
    base.OnCreate(bundle);

    global::Xamarin.Forms.Forms.Init(this, bundle);

    Uniforms.Amplitude.Droid.Amplitude.Register(this);

    // cross-platform interface, this code can be placed
    // to shared or portable application
    Amplitude.Instance.LogEvent("Start");

    LoadApplication(new App());
}
```

More info
---------

https://github.com/TheUniforms/Uniforms-Amplitude
