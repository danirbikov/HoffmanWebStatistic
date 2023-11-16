using HoffmanWebstatistic.Models.Hoffman;
using Quartz;
using ServicesWebAPI.Services;
using System.Net.NetworkInformation;

namespace HoffmanWebstatistic.Services.Job

{
    public static class Pinger
    {
        public static Dictionary<string, bool> standsPingResult { get; set; } = new Dictionary<string, bool>() { { "test", true } };


        public static Task PingAllStands(IEnumerable<Stand> allStands)
        {
            foreach (Stand stand in allStands)
            {
                bool connection_status = PingOneStand(stand);

                if (standsPingResult == null || !standsPingResult.ContainsKey(stand.StandName))
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
                LoggerTXT.LogError("Exception in pinging stand: \n" + stand.StandName + "\n" + stand.IpAdress + "\n" + ex);
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