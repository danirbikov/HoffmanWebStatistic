using Microsoft.AspNetCore.Mvc;
using MVCENG2.Interfaces;
using MVCENG2.Models;
using System.Buffers;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;

namespace MVCENG2.Services
{
    public class Pinger
    {
        public string IP_adress;
        private readonly IStandRepository _standRepository;
        public Pinger(IStandRepository standRepository)
        {
            _standRepository = standRepository;
        }
        public async Task PingAllStands()
        {
            IEnumerable<Stand> stands = await _standRepository.GetAll();
            foreach (Stand stand in stands)
            {
                bool connection_status = PingOneStand(stand);

            }
        }
        public bool PingOneStand(Stand stand)
        {  
            Ping pinger = new Ping();
            bool pingable = false;
            try
            {                            
                PingReply reply = pinger.Send(stand.IP_adress);
                pingable = reply.Status == IPStatus.Success;
                
            }
            catch (PingException)
            {
                using (StreamWriter writer = new StreamWriter("Logs/PingerLogs.txt"))
                {
                    writer.WriteLine("Exception in pinging stand: ");
                    writer.WriteLine(stand.Stand_name);
                    writer.WriteLine(stand.IP_adress);
                    
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
            if (pingable)
                stand.Connection_status = "OK";
            else
                stand.Connection_status = "NOK";

            _standRepository.Update(stand);
            return pingable;
        }






    }

}

