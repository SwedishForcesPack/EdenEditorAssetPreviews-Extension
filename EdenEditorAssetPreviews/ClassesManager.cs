using System;
using System.Collections.Generic;

namespace EdenEditorAssetPreviews
{
    class ClassesManager
    {
        private List<string> _addons = new List<string>();
        private List<string> _classes = new List<string>();

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
            _classes.Add(newClass);
        }

        public IEnumerable<string> GetClasses()
        {
            return _classes;
        }
    }
}
