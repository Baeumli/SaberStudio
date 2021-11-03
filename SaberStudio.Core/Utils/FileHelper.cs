using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace SaberStudio.Core.Util
{
    public static class FileHelper
    {
        public static void CreateFolder(string folderPath)
        {
            if (string.IsNullOrWhiteSpace(folderPath))
                throw new ArgumentNullException("parameter is empty");

            // Checking if the folder exists is not necessary
            // See: https://docs.microsoft.com/en-us/dotnet/api/system.io.directory.createdirectory

            Directory.CreateDirectory(folderPath);
        }
        
        public static bool FolderExists(string folderPath)
        {
            if (string.IsNullOrWhiteSpace(folderPath))
                throw new ArgumentNullException("parameter is empty");

            return Directory.Exists(folderPath);
        }
        
        public static void DeleteFolder(string folderPath, bool recursive)
        {
            if (string.IsNullOrWhiteSpace(folderPath))
                throw new ArgumentNullException("parameter is empty");

            if (FolderExists(folderPath))
                Directory.Delete(folderPath, recursive);
        }
        
        public static async Task CreateFileFromStream(Stream stream, string outputPath, bool overwrite)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            if (!stream.CanRead)
                throw new NotSupportedException("Cannot read from unsupported stream");

            if (string.IsNullOrWhiteSpace(outputPath))
                throw new ArgumentNullException("parameter is empty");

            if (FileExists(outputPath) && overwrite == false)
                return;

            await using (var fileStream = File.Create(outputPath))
            {
                await stream.CopyToAsync(fileStream);
            }
        }
        
        public static bool FileExists(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentNullException("parameter is empty");

            return File.Exists(filePath);
        }
        
        public static void DeleteFile(string filePath)
        {
            try
            {
                File.Delete(filePath);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }
        
        public static void ExtractZip(string archive, string savePath, IEnumerable<string> fileHashes)
        {
            try
            {
                var zipHashes = new List<string>();

                using (var zip = ZipFile.OpenRead(archive))
                {
                    foreach (var zippedFile in zip.Entries)
                    {
                        using (var md5 = MD5.Create())
                        {
                            var buffer = md5.ComputeHash(zippedFile.Open());
                            var hash = BitConverter.ToString(buffer).Replace("-", "").ToLowerInvariant();
                            zipHashes.Add(hash);
                        }
                    }

                    // If hashes don't match
                    if (fileHashes.Except(zipHashes).Any())
                        throw new InvalidDataException();

                    zip.ExtractToDirectory(savePath, true);
                }
                DeleteFile(archive);
            }
            catch (InvalidDataException ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public static string CalculateMD5(string filePath)
        {
            using var md5 = MD5.Create();
            using var stream = File.OpenRead(filePath);
            var hash = md5.ComputeHash(stream);
            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        }
        
    }
}