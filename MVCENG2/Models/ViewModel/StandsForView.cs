using MVCENG2.Models.General;

namespace MVCENG2.Models.ViewModel
{
    public class StandsForView
    {
        public IEnumerable<Stand> stands { get; set; }
        public Dictionary<string, bool> pingerDict { get; set; }
        public Dictionary<string, int> testsLastMonth { get; set; }
    }
}