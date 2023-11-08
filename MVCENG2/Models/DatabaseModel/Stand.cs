using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HoffmanWebstatistic.Models.Hoffman
{
    public partial class Stand
    {
        public Stand()
        {
            Mes2supPaths = new HashSet<Mes2supPath>();
            Mes2supTelegramsStands = new HashSet<Mes2supTelegramsStand>();
            OperatorsStands = new HashSet<OperatorsStand>();
            PicturesPaths = new HashSet<DTCPaths>();
            ResultsJsonHeaders = new HashSet<ResultsJsonHeader>();
            Sup2mesPaths = new HashSet<Sup2mesPath>();
            Sup2mesTelegrams = new HashSet<Sup2mesTelegram>();
            TranslatesPaths = new HashSet<TranslatesPath>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Некорректное значение")]
        [MaxLength(50, ErrorMessage = "Длина не должна превышать 50 символов")]
        public string StandName { get; set; } = null!;
        [Required(ErrorMessage = "Некорректное значение")]
        [MaxLength(255, ErrorMessage = "Длина не должна превышать 255 символов")]
        public string StandNameDescription { get; set; } = null!;
        [Required(ErrorMessage = "Некорректное значение")]
        [MaxLength(255, ErrorMessage = "Длина не должна превышать 255 символов")]
        public string WorkplaceMes { get; set; } = null!;
        [Required(ErrorMessage = "Некорректное значение")]
        [MaxLength(50, ErrorMessage = "Длина не должна превышать 50 символов")]
        [RegularExpression(@"^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$", ErrorMessage = "Invalid IP address")]
        public string IpAdress { get; set; } = null!;
        [Required(ErrorMessage = "Некорректное значение")]
        [MaxLength(50, ErrorMessage = "Длина не должна превышать 50 символов")]
        public string DnsName { get; set; } = null!;
        [Required(ErrorMessage = "Некорректное значение")]
        [MaxLength(50, ErrorMessage = "Длина не должна превышать 50 символов")]
        public string Placement { get; set; } = null!;
        [Required(ErrorMessage = "Некорректное значение")]
        public int OSVersionNavigationID { get; set; }
        [Required(ErrorMessage = "Некорректное значение")]
        [MaxLength(50, ErrorMessage = "Длина не должна превышать 50 символов")]
        public string StandType { get; set; } = null!;
        [Required(ErrorMessage = "Некорректное значение")]
        [MaxLength(20, ErrorMessage = "Длина не должна превышать 20 символов")]
        public string Project { get; set; } = null!;
        [Required(ErrorMessage = "Некорректное значение")]
        [MaxLength(5, ErrorMessage = "Длина не должна превышать 5 символов")]
        public string InactiveMark { get; set; } = null!;

        public virtual OsVersions OsVersionNavigation { get; set; } = null!;
        public virtual ICollection<Mes2supPath> Mes2supPaths { get; set; }
        public virtual ICollection<Mes2supTelegramsStand> Mes2supTelegramsStands { get; set; }
        public virtual ICollection<OperatorsStand> OperatorsStands { get; set; }
        public virtual ICollection<DTCPaths> PicturesPaths { get; set; }
        public virtual ICollection<ResultsJsonHeader> ResultsJsonHeaders { get; set; }
        public virtual ICollection<Sup2mesPath> Sup2mesPaths { get; set; }
        public virtual ICollection<Sup2mesTelegram> Sup2mesTelegrams { get; set; }
        public virtual ICollection<SendingStatusLog> SendingStatusLogs { get; set; }
        public virtual ICollection<TranslatesPath> TranslatesPaths { get; set; }
        public virtual ICollection<DtcsPath> DtcsPaths { get; set; }
        public virtual ICollection<OperatorsPath> OperatorsPaths { get; set; }
        public virtual ICollection<JsonsPath> JsonsPaths { get; set; }
    }
}
