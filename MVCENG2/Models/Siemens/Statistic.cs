using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MVCENG2.Models.Siemens
{

    public class Statistic
    {

        public string? ProductionNumber { get; set; }
        [Key]
        public string? VIN { get; set; }
        public string? TestStart { get; set; }
        public string? TestEnd { get; set; }
        public string? TotalDuration { get; set; }
        public string? Result { get; set; }
        public string? NotOks { get; set; }
        public string? Client { get; set; }
        public string? TestType { get; set; }
    }
}
