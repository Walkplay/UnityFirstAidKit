using PackageHandler.Interfaces;
using Ionic.Zip;
using System.Linq;

namespace PackageHandler.DecompressModule
{
    public class ZipArchivator : IArchivator
    {
        public string GetNestedFileName(string zipFilePath)
        {
            using (ZipFile zip = ZipFile.Read(zipFilePath))
            {
                return zip.Entries.First().FileName;
            }
        }

        public void Decompress(string fileToDecompress, string destinationPath)
        {
            using (ZipFile zip = ZipFile.Read(fileToDecompress))
            {
                foreach (ZipEntry e in zip)
                {
                    e.Extract(destinationPath, ExtractExistingFileAction.OverwriteSilently);  // overwrite existing files
                }

            }
        }

        public void Compress(string fileToCompress, string destinationPath = null)
        {
            using (ZipFile zip = new ZipFile())
            {
                ZipEntry ze = zip.AddItem(fileToCompress);
                if (destinationPath == null)
                    zip.Save(fileToCompress + ".zip");
                else
                    zip.Save(destinationPath);
            }
        }

    }
}
