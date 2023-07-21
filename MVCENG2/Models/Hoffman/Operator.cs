using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVCENG2.Models.Hoffman
{
    public partial class Operator
    {
        public Operator()
        {
            OperatorsStands = new HashSet<OperatorsStand>();
            ResultsJsonHeaders = new HashSet<ResultsJsonHeader>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Некорректное значение")]
        [MaxLength(50, ErrorMessage = "Длина не должна превышать 50 символов")]
        public string OLogin { get; set; } = null!;
        [Required(ErrorMessage = "Некорректное значение")]
        [MaxLength(50, ErrorMessage = "Длина не должна превышать 50 символов")]
        public string OPassword { get; set; } = null!;
        [Required(ErrorMessage = "Некорректное значение")]
        [MaxLength(1024, ErrorMessage = "Длина не должна превышать 1024 символов")]
        public string ODescription { get; set; } = null!;
        public DateTime Created { get; set; }
        public string InactiveMark { get; set; } = null!;

        public virtual ICollection<OperatorsStand> OperatorsStands { get; set; }
        public virtual ICollection<ResultsJsonHeader> ResultsJsonHeaders { get; set; }
    }
}
