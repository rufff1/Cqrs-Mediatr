using Busines.DTOs.Product.Response;
using Business.DTOs.Category.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Business.DTOs.Category.Response
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }



        public List<ProductDTO> Products { get; set; }

    }
}
