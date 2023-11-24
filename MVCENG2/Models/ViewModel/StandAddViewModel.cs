using HoffmanWebstatistic.Models.Hoffman;

namespace HoffmanWebstatistic.Models.ViewModel
{
    public class StandAddViewModel
    {
        public Stand stand { get; set; }
        public Stand QNX { get; set; }
        public bool checkbox { get; set; }
        public DtcsPath dtcPath { get; set; }
        public JsonsPath jsonsPath { get; set; }
        public Mes2supPath mes2SupPath { get; set; }
        public OperatorsPath operatorsPath { get; set; }
        public PicturesPath picturesPath { get; set; }
        public Sup2mesPath sup2mesPath { get; set; }
        public TranslatesPath translatesPath { get; set; } 




    }
}