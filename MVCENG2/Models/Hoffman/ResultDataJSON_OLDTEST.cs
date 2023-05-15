using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCENG2.Models.Hoffman
{
    public class ResultDataJSON_OLDTEST
    {
        [Key]
        public long id { get; set; }
        public string vin { get; set; }
        public string ordernum { get; set; }
        public string json_filename { get; set; }
        public int stand_id { get; set; }
        public DateTime created { get; set; }
        public int operator_id { get; set; }
        public byte[] tg_content { get; set; }


    }
}
