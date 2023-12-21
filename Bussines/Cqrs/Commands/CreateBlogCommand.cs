using Busines.DTOs.Blog.Request;
using Bussines.DTOs.Common;
using MediatR;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Busines.Cqrs.Commands
{
    public class CreateBlogCommand :IRequest<Response<BlogCreateDTO>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public int CategoryId { get; set; }


        [NotMapped]
        [MaxLength(3)]
        public List<int> TagIds { get; set; }   



    }
}
