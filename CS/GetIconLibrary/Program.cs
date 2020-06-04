using System;
using System.Drawing;
using System.IO;
using DevExpress.Images;
using DevExpress.Utils.Design;
using DevExpress.Utils.Svg;

namespace GetIconLibrary
{

    class Program
    {
        static void Main(string[] args)
        {

            string filePath = @"c:\DevExpressIcons\";

            if (!Directory.Exists(filePath))
            {
                try
                {
                    Directory.CreateDirectory(filePath);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Can't create a directory: {0}", e.ToString());
                }
                finally { }
            }


            StoreImagesByImageType(filePath, ImageType.Colored);

        }

        public static void StoreImagesByImageType(string filePath, ImageType imageType)
        {
            foreach (ImagesAssemblyImageInfo imageInfo in ImagesAssemblyImageList.Images)
            {
                var stream = ImageResourceCache.Default.GetResourceByFileName(imageInfo.Name, imageType);
                if (stream == null) continue;
                var newFilePath = Path.Combine(filePath, imageInfo.ImageType.ToString());
                if (!Directory.Exists(newFilePath))
                {
                    Directory.CreateDirectory(newFilePath);
                }
                if (imageInfo.ImageType != ImageType.Svg)
                {
                    using (Image image = Image.FromStream(stream))
                    {
                        image.Save(Path.Combine(newFilePath, imageInfo.Name));
                    }
                }
                else
                {
                    SvgImage image = SvgImage.FromStream(stream);
                    image.Save(Path.Combine(newFilePath, imageInfo.Name));
                }
                Console.WriteLine("Writing " + imageInfo.Name);
            }
        }
    }
}
