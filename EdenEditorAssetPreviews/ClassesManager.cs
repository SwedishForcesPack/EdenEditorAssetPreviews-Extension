using System;
using System.Collections.Generic;

namespace EdenEditorAssetPreviews
{
    class ClassesManager
    {
        private List<string> _addons = new List<string>();
        private List<ConfigClass> _classes = new List<ConfigClass>();

        public void AddAddon(string newAddon)
        {
            _addons.Add(newAddon);
        }

        public IEnumerable<string> GetAddons()
        {
            return _addons;
        }

        public void AddClass(string newClass, string inheritedClass)
        {
            _classes.Add(new ConfigClass(newClass, inheritedClass));
        }

        public IEnumerable<ConfigClass> GetClasses()
        {
            var resolvedClasses = new HashSet<string>(GetExternalReferences());
            var classesToResolve = new List<ConfigClass>(_classes);
            var orderedClasses = new List<ConfigClass>();

            while (classesToResolve.Count > 0)
            {
                foreach (var configClass in classesToResolve)
                {
                    if (resolvedClasses.Contains(configClass.Inherits))
                    {
                        orderedClasses.Add(configClass);
                        resolvedClasses.Add(configClass.Name);
                    }
                }

                int processedClasses = classesToResolve.RemoveAll(x => orderedClasses.Contains(x));

                if (processedClasses == 0)
                {
                    throw new Exception("No classes could be resolved");
                }
            }

            return orderedClasses;
        }

        public IEnumerable<string> GetExternalReferences()
        {
            var externalReferences = new HashSet<string>();

            foreach (ConfigClass configClass in _classes)
            {
                externalReferences.Add(configClass.Inherits);
            }

            foreach (ConfigClass configClass in _classes)
            {
                externalReferences.Remove(configClass.Name);
            }

            return externalReferences;
        }
    }
}
