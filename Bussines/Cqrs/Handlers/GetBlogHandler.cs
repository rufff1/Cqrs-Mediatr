using AutoMapper;
using Busines.Cqrs.Queries;
using Busines.DTOs.Blog.Response;
using Busines.Exceptions;
using Bussines.DTOs.Common;
using DataAccess.Repositories.Abstract;
using DataAccess.UnitOfWork;
using MediatR;


namespace Busines.Cqrs.Handlers
{
    public class GetBlogHandler : IRequestHandler<GetBlogByIdQuery, Response<BlogDTO>>
    {

        public readonly IBlogRepository _blogRepository;
        public readonly IMapper _mapper;

        public GetBlogHandler(IBlogRepository blogRepository, IUnitOfWork unitOfWork,IMapper mapper)
        {
            _blogRepository = blogRepository;
            _mapper =mapper;
        }
        public async Task<Response<BlogDTO>> Handle(GetBlogByIdQuery request, CancellationToken cancellationToken)
        {
            var result  = await _blogRepository.GetAsync(request.Id);
            if (result == null)
            {
                throw new NotFoundException("blog tapilmadi");
            }


            return new Response<BlogDTO>
            {
                Data = _mapper.Map<BlogDTO>(result),
                Message = "blog tapildi"
            };
        }
    }
}
