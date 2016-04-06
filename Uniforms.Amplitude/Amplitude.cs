using System;
using System.Collections.Generic;

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

        static Type _implementationClass;

        public static void Register(Type implementationClass)
        {
            if (_implementationClass == null)
            {
                _implementationClass = implementationClass;
            }
        }

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
                var inst = Activator.CreateInstance(_implementationClass) as IAmplitude;
                inst.Name = name;
                _namedInstances.Add(name, inst);
            }

            return _namedInstances[name];
        }
    }
}

