using HoffmanWebstatistic.Models.General;
using HoffmanWebstatistic.Models.Hoffman;

namespace HoffmanWebstatistic.Models.ViewModel
{
    public class StandsForAdminPanelView
    {
        public Stand stand { get; set; }
        public bool pingResult { get; set; }
        public DateTime lastTestDate { get; set; }
        public int allTestsCount { get; set; }
        
    }
}
