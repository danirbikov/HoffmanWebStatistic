using HoffmanWebstatistic.Models.General;

namespace HoffmanWebstatistic.Models.ViewModel
{
    public class SendingStatusLogViewModel
    {
        public IEnumerable<SendingStatusLog> sendingStatusLog { get; set; }
        public Dictionary<string, bool> pingerDict { get; set; }
    }
}