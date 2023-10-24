using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSTask2.Application.Dtos
{
    public class ShowProductDto
    {
        public string Name { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime ProduceDate { get; set; }
    }
}
