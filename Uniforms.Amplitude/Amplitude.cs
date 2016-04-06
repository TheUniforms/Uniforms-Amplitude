using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Uniforms.Amplitude
{
    public static class Amplitude
    {
        //
        // Instances
        //

        static readonly Dictionary<string, IAmplitude> _namedInstances =
            new Dictionary<string, IAmplitude>();

        //
        // Public static methods
        //

        public static IAmplitude Instance
        {
            get
            {
                return GetInstanceWithName("");
            }
        }

        public static IAmplitude GetInstanceWithName(string name)
        {
            if (!_namedInstances.ContainsKey(name))
            {
                var inst = DependencyService.Get<IAmplitude>();
                inst.Name = name;
                _namedInstances.Add(name, inst);
            }

            return _namedInstances[name];
        }
    }
}

