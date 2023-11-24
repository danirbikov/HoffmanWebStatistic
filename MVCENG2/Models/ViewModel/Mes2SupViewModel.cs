using HoffmanWebstatistic.Models.Hoffman;

namespace HoffmanWebstatistic.Models.ViewModel
{
    public class Mes2SupViewModel
    {
        
        public IEnumerable<Mes2supTelegram> sup2MesTelegrams { get; }
        public PageViewModel PageViewModel { get; }
        public SortViewModel SortViewModel { get; }
        public Mes2SupViewModel(IEnumerable<Mes2supTelegram> sup2MesTelegrams, PageViewModel pageViewModel, SortViewModel sortViewModel)
        {
            this.sup2MesTelegrams = sup2MesTelegrams;
            PageViewModel = pageViewModel;
            SortViewModel = sortViewModel;

        }
    }
}