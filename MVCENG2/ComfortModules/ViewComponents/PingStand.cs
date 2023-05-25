using Microsoft.AspNetCore.Mvc;
using MVCENG2.Models.General;
using System.Net.NetworkInformation;

namespace MvcApp.Components
{
    [ViewComponent]
    public class PingStand
    {
        public bool Invoke(string ipAddress)
        {
            Ping pinger = new Ping();
            bool pingable = false;
            try
            {
                PingReply reply = pinger.Send(ipAddress);
                pingable = reply.Status == IPStatus.Success;

            }
            catch (PingException)
            {
                using (StreamWriter writer = new StreamWriter("Logs/PingerLogs.txt"))
                {
                    writer.WriteLine("Exception in pinging stand: ");
                    writer.WriteLine(ipAddress);
                    writer.WriteLine();
                }
            }
            finally
            {
                if (pinger != null)
                {
                    pinger.Dispose();
                }
            }

            return pingable;

        }
    }
}