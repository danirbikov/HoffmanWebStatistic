using HoffmanWebstatistic.Models.Hoffman;

namespace HoffmanWebstatistic.Models.ViewModel
{
    public class StandsForView
    {
        public IEnumerable<Stand> stands { get; set; }
        public Dictionary<string, bool> pingerDict { get; set; }
        public Dictionary<string, int> testsLastMonth { get; set; }
    }
}