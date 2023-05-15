using System;
using System.Collections.Generic;
using MVCENG2.Models.Hoffman;

namespace MVCENG2.Models.General
{
    public partial class Stand
    {
        public Stand()
        {
            Mes2supPaths = new HashSet<Mes2supPath>();
            Mes2supTelegramsStands = new HashSet<Mes2supTelegramsStand>();
            OperatorsStands = new HashSet<OperatorsStand>();
            PicturesPaths = new HashSet<PicturesPath>();
            ResultsJsonHeaders = new HashSet<ResultsJsonHeader>();
            Sup2mesPaths = new HashSet<Sup2mesPath>();
            Sup2mesTelegrams = new HashSet<Sup2mesTelegram>();
            TranslatesPaths = new HashSet<TranslatesPath>();
        }

        public int Id { get; set; }
        public string StandName { get; set; } = null!;
        public string StandNameDescription { get; set; } = null!;
        public string WorkplaceMes { get; set; } = null!;
        public string IpAdress { get; set; } = null!;
        public string DnsName { get; set; } = null!;
        public string Placement { get; set; } = null!;
        public int OSVersionNavigationID { get; set; }
        public string StandType { get; set; } = null!;
        public string Project { get; set; } = null!;
        public string InactiveMark { get; set; } = null!;

        public virtual OsVersions OsVersionNavigation { get; set; } = null!;
        public virtual ICollection<Mes2supPath> Mes2supPaths { get; set; }
        public virtual ICollection<Mes2supTelegramsStand> Mes2supTelegramsStands { get; set; }
        public virtual ICollection<OperatorsStand> OperatorsStands { get; set; }
        public virtual ICollection<PicturesPath> PicturesPaths { get; set; }
        public virtual ICollection<ResultsJsonHeader> ResultsJsonHeaders { get; set; }
        public virtual ICollection<Sup2mesPath> Sup2mesPaths { get; set; }
        public virtual ICollection<Sup2mesTelegram> Sup2mesTelegrams { get; set; }
        public virtual ICollection<TranslatesPath> TranslatesPaths { get; set; }
    }
}
