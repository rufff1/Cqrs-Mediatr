

using Busines.DTOs.Blog.Request;
using Bussines.DTOs.Common;
using MediatR;

namespace Busines.Cqrs.Commands
{
    public class UpdateBlogCommand : IRequest<Response<BlogUpdateDTO>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }


        public int CategoryId { get; set; }
    }
}
