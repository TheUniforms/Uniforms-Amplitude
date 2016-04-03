#!/bin/sh

# Usage example:
# ./build.sh -o ~/Library/NuGet/

while [[ $# > 1 ]]
do
key="$1"

case $key in
    -o|--out-dir)
    OUTPUT_DIR="$2"
    shift # past argument
    ;;
    *)
    # unknown option
    ;;
esac
done

rm Uniforms.Amplitude*.dll 2> /dev/null
rm Uniforms.Amplitude*.mdb 2> /dev/null
rm Uniforms.Amplitude*.nupkg 2> /dev/null
cp ../Uniforms.Amplitude/bin/Release/Uniforms.Amplitude.dll* . 2> /dev/null
cp ../Uniforms.Amplitude.Droid/bin/Release/Uniforms.Amplitude.Droid.dll* . 2> /dev/null
cp ../Uniforms.Amplitude.iOS/bin/Release/Uniforms.Amplitude.iOS.dll* . 2> /dev/null

nuget pack Uniforms.Amplitude.nuspec

if [ $OUTPUT_DIR ]
then
echo "Moving *.nupkg -> $OUTPUT_DIR"
mv *.nupkg $OUTPUT_DIR
fi
