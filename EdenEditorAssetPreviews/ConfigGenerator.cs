using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EdenEditorAssetPreviews
{
    class ConfigGenerator
    {
        private IEnumerable<string> _patches;
        private IEnumerable<string> _classes;
        private string _prefix;

        public ConfigGenerator(IEnumerable<string> patches, IEnumerable<string> classes, string prefix)
        {
            _patches = patches;
            _classes = classes;
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
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("class CfgPatches");
            builder.AppendLine("{");
            builder.AppendLine("  class AssetPreviews");
            builder.AppendLine("  {");
            builder.AppendLine("    requiredVersion = 1.60;");
            if (_patches.Count() > 0)
            {
                builder.AppendLine("    requiredAddons[] = {\"" + String.Join("\",\"", _patches.ToArray()) + "\"};");
            }
            builder.AppendLine("  };");
            builder.AppendLine("};");
            return builder.ToString();
        }

        private string GenerateCfgVehicles()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("class CfgVehicles");
            builder.AppendLine("{");

            foreach (string klass in _classes)
            {
                builder.AppendLine("  class " + klass);
                builder.AppendLine("  {");
                builder.AppendLine("    editorPreview = \"" + _prefix + "\\ui\\" + klass + ".jpg\";");
                builder.AppendLine("  };");
            }

            builder.AppendLine("};");
            return builder.ToString();
        }
    }
}
