using System.Diagnostics;

namespace HoffmanWebstatistic.ComfortModules
{
    public class CmdOperations
    {
        public void DeleteCredentialForFolder(string folderPath)
        {
            using (Process process = new Process())
            {
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.Arguments = $"net use {folderPath} /delete";
                process.Start();

                if (!process.HasExited)
                {
                    process.Kill();
                    process.WaitForExit();
                }
            }
        }

        public void DeleteAllCredentials()
        {
            using (Process process = new Process())
            {
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.Arguments = $"net use * /delete";
                process.Start();

                if (!process.HasExited)
                {
                    process.Kill();
                    process.WaitForExit();
                }
            }
        }
    }
}
