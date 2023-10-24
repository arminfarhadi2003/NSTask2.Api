using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSTask2.Domin.Models
{
    public class Captcha
    {
        [Key]
        public int Id { get; set; }
        public DateTime TimeExp { get; set; }
        public bool Exp { get; set; }
        public string Value { get; set; }
    }
}
