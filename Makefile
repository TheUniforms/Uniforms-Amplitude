# MDTOOL ?= /Applications/Xamarin\ Studio.app/Contents/MacOS/mdtool
MDTOOL ?= /Applications/Visual\ Studio.app/Contents/MacOS/vstool
OUTPUT_DIR ?= lib
COMPONENT_DIR ?= component
COMPONENT_VERSION = 0.9.8
CURRENT_DIR = $(shell pwd)

.PHONY: all clean package component solution

all: component

clean:
	$(MDTOOL) build -c:Release -t:Clean Uniforms.Amplitude.sln
	rm -fv lib/Uniforms.*
	rm -fv component/*.xam

package: solution
	cp -v Uniforms.Amplitude/bin/Release/*.dll* $(OUTPUT_DIR)
	mono $(MONO_OPTIONS) vendor/nuget/Nuget.exe pack -OutputDirectory $(OUTPUT_DIR)

component: package
	cp -v Uniforms.Amplitude.Droid/bin/Release/*.dll* $(OUTPUT_DIR)
	cp -v Uniforms.Amplitude.Native.Droid/bin/Release/*.dll* $(OUTPUT_DIR)
	cp -v Uniforms.Amplitude.iOS/bin/Release/*.dll* $(OUTPUT_DIR)
	cp -v Uniforms.Amplitude.Native.iOS/bin/Release/*.dll* $(OUTPUT_DIR)
	cd $(COMPONENT_DIR) && mono $(CURRENT_DIR)/vendor/bin/xamarin-component.exe package

install:
	cd $(COMPONENT_DIR) && mono xamarin-component.exe install \
		uniforms-amplitude-$(COMPONENT_VERSION).xam

solution:
	$(MDTOOL) build -c:Release Uniforms.Amplitude.sln

