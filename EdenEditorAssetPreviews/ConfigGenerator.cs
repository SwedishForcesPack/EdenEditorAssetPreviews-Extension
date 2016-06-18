using System.Collections.Generic;
using System.Text;

namespace EdenEditorAssetPreviews
{
    class ConfigGenerator
    {
        private List<string> _classes;
        private string _prefix;

        public ConfigGenerator(List<string> classes, string prefix)
        {
            _classes = classes;
            _prefix = prefix;
        }

        public new string ToString()
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
