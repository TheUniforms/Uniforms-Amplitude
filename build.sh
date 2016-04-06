#!/bin/sh

BUILD_TOOL='/Applications/Xamarin Studio.app/Contents/MacOS/mdtool'
OUTPUT_DIR='lib'

exec "$BUILD_TOOL" build -c:"Release"