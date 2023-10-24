using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSTask2.Application.Dtos
{
    public class RemoveProductDto
    {
        public int ProductId { get; set; }
        public string UserId { get; set; }
        public string UserRole { get; set; }
    }
}
