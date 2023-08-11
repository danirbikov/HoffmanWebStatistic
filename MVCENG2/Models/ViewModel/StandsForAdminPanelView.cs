using MVCENG2.Models.General;
using MVCENG2.Models.Hoffman;

namespace MVCENG2.Models.ViewModel
{
    public class StandsForAdminPanelView
    {
        public Stand stand { get; set; }
        public bool pingResult { get; set; }
        public DateTime lastTestDate { get; set; }
        public int allTestsCount { get; set; }
        
    }
}
