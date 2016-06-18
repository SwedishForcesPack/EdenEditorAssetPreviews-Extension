using RGiesecke.DllExport;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace EdenEditorAssetPreviews
{
    public class DllEntry
    {
        private static ClassesManager _classesManager;
        private static ImagesManager _imagesManager;

        private static string _mod;
        private static string _outputPath;
        private static string _prefix;

        /// <summary>
        /// Hook for Arma.</summary>
        /// <remarks>
        /// The following calls are allowed:
        /// - init
        /// - addClass;newClass;inheritedClass
        /// - setOutput:outputPath</remarks>
        [DllExport("_RVExtension@12", CallingConvention = System.Runtime.InteropServices.CallingConvention.Winapi)]
        public static void RVExtension(StringBuilder output, int outputSize, [MarshalAs(UnmanagedType.LPStr)] string function)
        {
            string response = "";

            string[] args = function.Split(';');
            string method = args[0];

            switch (method)
            {
                case "init":
                    {
                        _classesManager = new ClassesManager();
                        _imagesManager = new ImagesManager();
                        response = "initialized";
                        break;
                    }
                case "addAddon":
                    {
                        var newAddon = args[1];
                        _classesManager.AddAddon(newAddon);
                        response = "added addon: " + newAddon;
                        break;
                    }
                case "addClass":
                    {
                        var newClass = args[1];
                        var inheritedClass = args[2];
                        _classesManager.AddClass(newClass, inheritedClass);
                        response = "added class: " + newClass;
                        break;
                    }
                case "setMod":
                    {
                        if (args.Length < 2)
                        {
                            response = "No mod defined";
                            break;
                        }

                        _mod = args[1];
                        break;
                    }
                case "setOutputPath":
                    {
                        if (args.Length < 2)
                        {
                            response = "No output path defined";
                            break;
                        }

                        _outputPath = args[1];
                        break;
                    }
                case "setPrefix":
                    {
                        if (args.Length < 2)
                        {
                            response = "No prefix defined";
                            break;
                        }

                        _prefix = args[1];
                        break;
                    }
                case "setProfileName":
                    {
                        if (args.Length < 2)
                        {
                            response = "No profile name defined";
                            break;
                        }

                        if (_imagesManager == null)
                        {
                            response = "Extension not initialized";
                            break;
                        }

                        _imagesManager.ProfileName = args[1];
                        break;
                    }
                case "processConfig":
                    {
                        if (_classesManager == null)
                        {
                            response = "Extension not initialized";
                            break;
                        }

                        if (_mod == null)
                        {
                            response = "No mod defined";
                            break;
                        }

                        if (_outputPath == null)
                        {
                            response = "No output path defined";
                            break;
                        }

                        if (_prefix == null)
                        {
                            response = "No prefix defined";
                            break;
                        }

                        var imagesPath = Path.Combine(_outputPath, "ui");
                        Directory.CreateDirectory(_outputPath);

                        ConfigGenerator configGenerator = new ConfigGenerator(_classesManager.GetAddons(), _classesManager.GetClasses(), _prefix);
                        File.WriteAllText(
                            Path.Combine(_outputPath, "config.cpp"),
                            configGenerator.ToString()
                        );
                        response = "saved classes as config.cpp";
                        break;
                    }
                case "processImages":
                    {
                        if (_imagesManager == null)
                        {
                            response = "Extension not initialized";
                            break;
                        }

                        if (_mod == null)
                        {
                            response = "No mod defined";
                            break;
                        }

                        if (_outputPath == null)
                        {
                            response = "No output path defined";
                            break;
                        }

                        var imagesPath = Path.Combine(_outputPath, "ui");
                        Directory.CreateDirectory(_outputPath);
                        Directory.CreateDirectory(imagesPath);

                        _imagesManager.ProcessImages(_mod, imagesPath);
                        response = "processed images";
                        break;
                    }
                case "version":
                    {
                        response = "1.0";
                        break;
                    }
                default:
                    {
                        response = "unhandled method " + method;
                        break;
                    }
            }

            output.Append(response);
        }
    }
}
