using PingerAPI.Models;
using PingerAPI.Models.General;
using Quartz;
using System.Net.NetworkInformation;

namespace PingerWebAPI.Services

{
    public static class Pinger
    {
        public static Dictionary<string, bool> standsPingResult { get; set; } = new Dictionary<string, bool>() {{ "test", false}};


        public static Task PingAllStands(IEnumerable<Stand> allStands)
        {
            foreach (Stand stand in allStands)
            {
                bool connection_status = PingOneStand(stand);

                if (standsPingResult==null || !standsPingResult.ContainsKey(stand.StandName))
                {
                    standsPingResult.Add(stand.StandName, connection_status);
                }
                else
                {
                    standsPingResult[stand.StandName] = connection_status;
                }
            }
            return null;

        }

        public static bool PingOneStand(Stand stand)
        {
            Ping pinger = new Ping();
            bool pingable = false;
            try
            {
                PingReply reply = pinger.Send(stand.IpAdress);
                pingable = reply.Status == IPStatus.Success;

            }
            catch (PingException ex)
            {
                using (StreamWriter writer = new StreamWriter("//Logs//Pinger.txt"))
                {
                    writer.WriteLine("Exception in pinging stand: ");
                    writer.WriteLine(stand.StandName);
                    writer.WriteLine(stand.IpAdress);
                    writer.WriteLine(ex);

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