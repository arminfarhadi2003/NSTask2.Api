using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSTask2.Application.Dtos
{
    public class EditProductDto
    {
        public string Name { get; set; }
        public bool IsAvailable { get; set; }
        public string UserId { get; set; }
        public string UserRole { get; set; }
        public int ProductId { get; set; }
    }
}
