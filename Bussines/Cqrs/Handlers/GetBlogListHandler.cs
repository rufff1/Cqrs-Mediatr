

using AutoMapper;
using Busines.Cqrs.Queries;
using Busines.DTOs.Blog.Response;
using Busines.Exceptions;
using Bussines.DTOs.Common;
using DataAccess.Repositories.Abstract;
using MediatR;


namespace Busines.Cqrs.Handlers
{
    public class GetBlogListHandler : IRequestHandler<GetBlogListQuery, Response<List<BlogDTO>>>
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IMapper _mapper;

        public GetBlogListHandler(IMapper mapper,IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
            _mapper = mapper;
        }


        public async Task<Response<List<BlogDTO>>> Handle(GetBlogListQuery request, CancellationToken cancellationToken)
        {
            var result = await _blogRepository.GetAllAsync();

            if (result.Count() <= 0) 
            {
                throw new NotFoundException(" hec bir blog tapilmadi");
            }

            return new Response<List<BlogDTO>>
            {
                Data = _mapper.Map<List<BlogDTO>>(result),
                Message = "bloglar tapildi"
            };
        }
    }
}
