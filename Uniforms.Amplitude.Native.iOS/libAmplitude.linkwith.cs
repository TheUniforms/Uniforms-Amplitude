using ObjCRuntime;

[assembly: LinkWith ("libAmplitude.a", SmartLink = true, ForceLoad = true,
    Frameworks = "Foundation", LinkerFlags = "-lsqlite3")]
