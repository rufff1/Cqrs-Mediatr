
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs.Category.Request
{
    public class CategoryCreateDTO
    {
        public string Name { get; set; }
        public string Description { get; set; } 


    }
}
