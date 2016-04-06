#/bin/sh

mono xamarin-component.exe package
rm ~/Library/Caches/Xamarin/Components/uniforms-amplitude-0.9.xam
mono xamarin-component.exe install uniforms-amplitude-0.9.xam