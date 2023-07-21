using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CopyFileTest
{
    internal class Program
    {
        static void Main(string[] args)
        {

            try
            {
                var sourceFilePath = @"C:\\STAND_Results"; 
                var destFilePath = @"C:\\TestParser\\Destination\\";
                //string sourceFilePath;
                string fileName;
                List<string> IPs = new List<string>() { "10.194.100.23", "10.194.100.17", "10.194.100.21", "10.194.100.19", "10.194.100.26", "10.194.100.28" } ;
                Console.WriteLine("Start");

                foreach (string IP in IPs)
                {
                    sourceFilePath = @"\\" + IP + @"\PAtools\vsp0\data\log_data";

                    try
                    {
                        foreach (var file in Directory.GetFileSystemEntries(sourceFilePath, "*.json", SearchOption.AllDirectories).Where(k => DateTime.Now.Subtract(new FileInfo(k).CreationTime).Days <= 7))
                        //foreach (var file in Directory.GetFileSystemEntries(sourceFilePath, "*.json", SearchOption.AllDirectories))
                        {
                            Console.WriteLine(file);
                            fileName = new FileInfo(file).Name;

                            if (!File.Exists(destFilePath + fileName))
                            {
                                File.Copy(file, destFilePath + fileName, true);
                            }
                        }
                         break;
                    }
                   

                    catch
                    {
                        Console.WriteLine($"IP {IP} not access!!!!!!!!!!!!!");
                    }
                }  
                

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Console.WriteLine("END");
                Console.ReadKey();
            }


        }

    }
}