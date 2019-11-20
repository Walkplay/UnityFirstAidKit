using PackageHandler.Interfaces;
using System.IO.Compression;
using System.IO;

namespace PackageHandler.DecompressModule
{
    public class GzipArchivator : IArchivator
    {
        public string GetNestedFileName(string zipFilePath)
        {
            return string.Empty;
        }

        public void Decompress(string fileToDecompress, string destinationPath)
        {
            FileInfo fileInfo = new FileInfo(fileToDecompress);
            using (FileStream originalFileStream = fileInfo.OpenRead())
            {
                using (FileStream decompressedFileStream = File.Create(destinationPath))
                {
                    using (GZipStream decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(decompressedFileStream);
                    }
                }
            }
        }

        public void Compress(string fileToCompress, string destination = null)
        {
            FileInfo fileInfo = new FileInfo(fileToCompress);
            using (FileStream originalFileStream = fileInfo.OpenRead())
            {
                if ((File.GetAttributes(fileInfo.FullName) &
                    FileAttributes.Hidden) != FileAttributes.Hidden & fileInfo.Extension != ".gz")
                {
                    using (FileStream compressedFileStream = File.Create(fileInfo.FullName + ".gz"))
                    {
                        using (GZipStream compressionStream = new GZipStream(compressedFileStream, CompressionMode.Compress))
                        {
                            originalFileStream.CopyTo(compressionStream);
                        }
                    }
                }

            }

        }
    }
}
