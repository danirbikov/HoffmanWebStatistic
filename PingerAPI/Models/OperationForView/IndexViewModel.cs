using PingerAPI.Models.Hoffman;

namespace PingerAPI.Models.OperationForView
{
    public class IndexViewModel
    {
        public IEnumerable<ResultsJsonHeader> resultsJsonHeader { get; }
        public PageViewModel PageViewModel { get; }
        //public FilterViewModel FilterViewModel { get; }
        public SortViewModel SortViewModel { get; }
        public IndexViewModel(IEnumerable<ResultsJsonHeader> resultsJsonHeader, PageViewModel pageViewModel, SortViewModel sortViewModel)
        {
            this.resultsJsonHeader = resultsJsonHeader;
            PageViewModel = pageViewModel;
            SortViewModel = sortViewModel;
        }
    }
}