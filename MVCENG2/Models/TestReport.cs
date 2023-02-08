using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MVCENG2.Models
{


    public class TestReport
    {
        [Key]
        public int Id { get; set; }
        public string? Component { get; set; }
        public string? Result { get; set; }
        public string? LowerLimit { get; set; }
        public string? UpperLimit { get; set; }
        public string? MeasureValue { get; set; }
        public string? Unit { get; set; }
        public string? ResultValue { get; set; }
        public string? VIN { get; set; }
      

    }
}
