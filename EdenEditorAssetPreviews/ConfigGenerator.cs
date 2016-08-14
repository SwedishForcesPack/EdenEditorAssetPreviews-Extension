using System;
using System.Linq;
using System.Text;

namespace EdenEditorAssetPreviews
{
    class ConfigGenerator
    {
        private ClassesManager _classesManager;
        private string _patchesClass;
        private string _prefix;

        public ConfigGenerator(ClassesManager classesManager, string patchesClass, string prefix)
        {
            _classesManager = classesManager;
            _patchesClass = patchesClass;
            _prefix = prefix;
        }

        public new string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(GenerateCfgPatches());
            builder.Append(GenerateCfgVehicles());
            return builder.ToString();
        }

        private string GenerateCfgPatches()
        {
            var patches = _classesManager.GetAddons();

            StringBuilder builder = new StringBuilder();
            builder.AppendLine("class CfgPatches");
            builder.AppendLine("{");
            builder.AppendLine("  class " + _patchesClass);
            builder.AppendLine("  {");
            builder.AppendLine("    requiredVersion = 1.60;");
            builder.AppendLine("    requiredAddons[] = {");
            if (patches.Count() > 0)
            {
                builder.AppendLine("      \"" + String.Join("\",\n      \"", patches.ToArray()) + "\"");
            }
            builder.AppendLine("    };");
            builder.AppendLine("    units[] = {};");
            builder.AppendLine("    weapons[] = {};");
            builder.AppendLine("  };");
            builder.AppendLine("};");
            return builder.ToString();
        }

        private string GenerateCfgVehicles()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("class CfgVehicles");
            builder.AppendLine("{");

            foreach (var reference in _classesManager.GetExternalReferences())
            {
                builder.AppendLine("  class " + reference + ";");
            }

            foreach (var configClass in _classesManager.GetClasses())
            {
                if (configClass.Inherits != null)
                {
                    builder.AppendLine("  class " + configClass.Name + " : " + configClass.Inherits);
                } else
                {
                    builder.AppendLine("  class " + configClass.Name);
                }
                builder.AppendLine("  {");
                builder.AppendLine("    editorPreview = \"" + _prefix + "\\ui\\" + configClass.Name + ".jpg\";");
                builder.AppendLine("  };");
            }

            builder.AppendLine("};");
            return builder.ToString();
        }
    }
}
