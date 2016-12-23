# Getting Started with Uniforms.Amplitude

[**Amplitude**](https://amplitude.com) is a platform for web & mobile analytics that helps you drive retention, engagement, and conversion. And this component provides cross-platform Amplitude API bindings for Xamarin.

You will need an Amplitude account and application registered at Amplitude to use this component.

Plase note, the component developers aren't affiliated with Amplitude!


Quickstart
----------

1. Initialize platform specific classes:

    For **iOS** application in `UIApplicationDelegate.FinishedLaunching()`:

    `Uniforms.Amplitude.iOS.Amplitude.Register();`

    For **Android** application in `OnCreate()` of main activity:

    `Uniforms.Amplitude.Droid.Amplitude.Register(this);`

2. Initialize Amplitude with API key:

    `Amplitude.Instance.Initialize("PASTE YOUR API KEY HERE");`

3. Start sending events to Amplitude:

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

// ...

public override bool FinishedLaunching(UIApplication app, NSDictionary options)
{
    global::Xamarin.Forms.Forms.Init();

    LoadApplication(new App());

    Uniforms.Amplitude.iOS.Amplitude.Register();

    return base.FinishedLaunching(app, options);
}
```


For **Android**:

```csharp
// MainActivity.cs
// ...
using Uniforms.Amplitude;

// ...

protected override void OnCreate(Bundle bundle)
{
    base.OnCreate(bundle);

    global::Xamarin.Forms.Forms.Init(this, bundle);

    Uniforms.Amplitude.Droid.Amplitude.Register(this);

    LoadApplication(new App());
}
```

In shared application:

```csharp
using Xamarin.Forms;
using Uniforms.Amplitude;

// ...

protected override void OnStart()
{
    Amplitude.Instance.Initialize("PASTE YOUR API KEY HERE");

    Amplitude.Instance.LogEvent("Start");
}
```


More info
---------

**API reference**:  
https://components.xamarin.com/view/uniforms-amplitude

**Uniforms.Amplitude on GitHub**:  
https://github.com/TheUniforms/Uniforms-Amplitude
