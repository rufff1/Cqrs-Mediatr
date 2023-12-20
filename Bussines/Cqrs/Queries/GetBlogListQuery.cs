using Busines.DTOs.Blog.Response;
using Bussines.DTOs.Common;
using MediatR;


namespace Busines.Cqrs.Queries
{
    public class GetBlogListQuery : IRequest<Response<List<BlogDTO>>>
    {

    }
}
