Amplitude for Xamarin
=====================

[Amplitude](https://amplitude.com) API binding library for [Xamarin](https://www.xamarin.com).

Usage
-----

...

Build iOS bindings
------------------

1. Clone Amplitude-iOS from GitHub

    https://github.com/amplitude/Amplitude-iOS

2. Build Amplitude-iOS

```bash
$ cd Amplitude-iOS
$ xcodebuild
$ xcodebuild -sdk iphonesimulator -arch i386
$ lipo -create -output build/libAmplitude.a \
  build/Release-iphoneos/libAmplitude.a \
  build/Release-iphonesimulator/libAmplitude.a
```

This creates `build/libAmplitude.a` which will be a universal (fat) library which will be suitable to use for all iOS development targets.

3. Copy `libAmplitude.a` to `Uniforms.Amplitude.iOS` project.

4. Build project in Xamarin Studio (didn't tested with Visual Studio yet).

5. Build NuGet with `NuGet/build.sh`.
