using HoffmanWebstatistic.Models.Hoffman;

namespace HoffmanWebstatistic.Models.ViewModel
{
    public class Sup2MesViewModel
    {
        
        public IEnumerable<Sup2mesTelegram> sup2MesTelegrams { get; }
        public PageViewModel PageViewModel { get; }
        //public FilterViewModel FilterViewModel { get; }
        public SortViewModel SortViewModel { get; }
        public Sup2MesViewModel(IEnumerable<Sup2mesTelegram> sup2MesTelegrams, PageViewModel pageViewModel, SortViewModel sortViewModel)
        {
            this.sup2MesTelegrams = sup2MesTelegrams;
            PageViewModel = pageViewModel;
            SortViewModel = sortViewModel;

        }
    }
}