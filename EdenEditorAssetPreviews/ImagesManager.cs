using ImageProcessor;
using ImageProcessor.Imaging.Formats;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace EdenEditorAssetPreviews
{
    class ImagesManager
    {
        private readonly static int EDEN_EDITOR_PREVIEW_WIDTH = 455;
        private readonly static int EDEN_EDITOR_PREVIEW_HEIGHT = 256;

        public string ProfileName;

        public ImagesManager()
        {

        }

        public void ProcessImages(string mod, string outputPath)
        {
            var modDirectory = Path.Combine(EditorPreviewsPath(), mod);

            var images = Directory.EnumerateFiles(modDirectory);

            foreach (string image in images)
            {
                var destination = Path.Combine(outputPath, Path.GetFileNameWithoutExtension(image) + ".jpg");

                try
                {
                    ProcessImage(image, destination);
                    File.Delete(image);
                } catch
                {

                }
            }
        }

        private void ProcessImage(string inputImage, string outputImage)
        {
            byte[] photoBytes = File.ReadAllBytes(inputImage);

            ISupportedImageFormat format = new JpegFormat { Quality = 70 };
            Size size = new Size(EDEN_EDITOR_PREVIEW_WIDTH, EDEN_EDITOR_PREVIEW_HEIGHT);

            using (MemoryStream inStream = new MemoryStream(photoBytes))
            {
                using (MemoryStream outStream = new MemoryStream())
                {
                    using (ImageFactory imageFactory = new ImageFactory())
                    {
                        imageFactory.Load(inStream)
                                    .Resize(size)
                                    .Format(format)
                                    .Save(outStream);
                    }

                    using (FileStream file = new FileStream(outputImage, FileMode.Create, FileAccess.Write))
                    {
                        outStream.WriteTo(file);
                    }
                }
            }
        }

        private string EditorPreviewsPath()
        {
            var myDocumentsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var armaDocumentsDirectory = Path.Combine(myDocumentsDirectory, "Arma 3");
            var armaOtherProfilesDirectory = Path.Combine(myDocumentsDirectory, "Arma 3 - Other Profiles");

            var profileDirectory = armaDocumentsDirectory;

            if (ProfileName != null && ProfileName.Length > 0 && Directory.Exists(Path.Combine(armaOtherProfilesDirectory, ProfileName))) {
                profileDirectory = Path.Combine(armaOtherProfilesDirectory, ProfileName);
            }

            var screenshotsDirectory = Path.Combine(profileDirectory, "Screenshots");
            var editorPreviewsDirectory = Path.Combine(screenshotsDirectory, "EditorPreviews");

            return editorPreviewsDirectory;
        }
    }
}
