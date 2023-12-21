using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Busines.DTOs.Blog.Request
{
    public class BlogUpdateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }

        public int CategoryId { get; set; }


        [NotMapped]
        [MaxLength(3)]
        public List<int> TagIds { get; set; }
    }
}
