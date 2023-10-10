using System.Linq;
using System.Security.Cryptography;

namespace HoffmanWebstatistic.Services
{
    public static class Hashing
    {

        public static byte[] CalculateImageHash(byte[] imageBytes)
        {
            using (var algorithm = SHA256.Create()) // Используем алгоритм SHA256
            {
                return algorithm.ComputeHash(imageBytes);
            }
        }

        public static byte[] CalculateFileHash(string filePath)
        {
            using (var algorithm = SHA256.Create()) // Используем алгоритм SHA256
            {
                using (var stream = File.OpenRead(filePath))
                {
                    return algorithm.ComputeHash(stream);
                }
            }
        }

        public static bool CompareHashByteArrAndFile(byte[] imageByteArray, string imageFilePath)
        {
            bool areHashesEqual = CalculateImageHash(imageByteArray).SequenceEqual(CalculateFileHash(imageFilePath));

            if (areHashesEqual)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool CompareHashFiles(string imageFilePath1, string imageFilePath2)
        {
            bool areHashesEqual = CalculateFileHash(imageFilePath1).SequenceEqual(CalculateFileHash(imageFilePath2));

            if (areHashesEqual)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
