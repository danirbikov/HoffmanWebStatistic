using HoffmanWebstatistic.Models.Hoffman;

namespace HoffmanWebstatistic.Models.ViewModel
{
    public class ReportViewModel
    {
        
        public IEnumerable<ResultsJsonHeader> resultsJsonHeader { get; }
        public PageViewModel PageViewModel { get; }
        //public FilterViewModel FilterViewModel { get; }
        public SortViewModel SortViewModel { get; }
        public ReportViewModel(IEnumerable<ResultsJsonHeader> resultsJsonHeader, PageViewModel pageViewModel, SortViewModel sortViewModel)
        {
            this.resultsJsonHeader = resultsJsonHeader;
            PageViewModel = pageViewModel;
            SortViewModel = sortViewModel;
        }
    }
}