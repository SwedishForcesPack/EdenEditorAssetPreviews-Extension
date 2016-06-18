using System;
using System.Collections.Generic;

namespace EdenEditorAssetPreviews
{
    class ClassesManager
    {
        private List<string> _classes = new List<string>();

        public void AddClass(string newClass, string inheritedClass)
        {
            _classes.Add(newClass);
        }

        public List<string> GetClasses()
        {
            return _classes;
        }
    }
}
