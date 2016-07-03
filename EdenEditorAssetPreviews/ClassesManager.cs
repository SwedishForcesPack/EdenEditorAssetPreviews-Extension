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
            return _classes;
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
