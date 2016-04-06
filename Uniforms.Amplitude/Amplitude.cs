using System;
using System.Collections.Generic;

namespace Uniforms.Amplitude
{
    public static class Amplitude
    {
        //
        // Instances
        //

        static IAmplitude _defaultInstance;

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

        /// <summary>
        /// Gets the default Amplitude instance.
        /// </summary>
        public static IAmplitude Instance
        {
            get
            {
                return GetInstanceWithName(null);
            }
        }

        /// <summary>
        /// Get Amplitude instance with identifier.
        /// </summary>
        public static IAmplitude GetInstanceWithName(string name)
        {
            // Default instance
            if (String.IsNullOrEmpty(name))
            {
                if (_defaultInstance == null)
                {
                    _defaultInstance = Activator.CreateInstance(_implementationClass) as IAmplitude;
                    _namedInstances.Add(_defaultInstance.InstanceName, _defaultInstance);
                }

                return _defaultInstance;
            }

            // Named instance
            if (!_namedInstances.ContainsKey(name))
            {
                var instance = Activator.CreateInstance(_implementationClass) as IAmplitude;
                instance.InstanceName = name;
                _namedInstances.Add(name, instance);
            }

            return _namedInstances[name];
        }
    }
}

