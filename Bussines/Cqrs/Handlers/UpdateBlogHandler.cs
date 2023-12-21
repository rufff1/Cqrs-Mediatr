
using AutoMapper;
using Busines.Cqrs.Commands;
using Busines.DTOs.Blog.Request;
using Busines.Exceptions;
using Busines.Validators.Blog;
using Bussines.DTOs.Common;
using Common.Entities;
using DataAccess.Context;
using DataAccess.Repositories.Abstract;
using DataAccess.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Busines.Cqrs.Handlers
{
    public class UpdateBlogHandler : IRequestHandler<UpdateBlogCommand, Response<BlogUpdateDTO>>
    {
        public readonly IBlogRepository _blogRepository;
        public readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;
        public readonly AppDbContext _context;

        public UpdateBlogHandler(AppDbContext context,IBlogRepository blogRepository,IUnitOfWork unitOfWork,IMapper mapper)
        {
            _blogRepository = blogRepository;
            _unitOfWork = unitOfWork;   
            _mapper =mapper;
            _context = context;
        }

        public async Task<Response<BlogUpdateDTO>> Handle(UpdateBlogCommand request, CancellationToken cancellationToken)
        {
            var result = await new BlogUpdateValidator().ValidateAsync(request);

            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }


            var existBlog = await _blogRepository.GetAsync(request.Id);

            if (existBlog == null)
            {
                throw new NotFoundException("blog tapilmadi");
            }


            if (!await _context.Blogs.AnyAsync(t => t.Id == existBlog.CategoryId))
            {
                throw new ValidationException("duzgun category secin");
            }

            var entity = _mapper.Map(request,existBlog);

            existBlog.ModifiedDate = DateTime.Now;


            _blogRepository.Update(existBlog);
            await _unitOfWork.CommitAsync(cancellationToken);



            return new Response<BlogUpdateDTO>
            {
                Data = _mapper.Map<BlogUpdateDTO>(entity),
                Message = "update olundu"
            };



        }
    }
}
