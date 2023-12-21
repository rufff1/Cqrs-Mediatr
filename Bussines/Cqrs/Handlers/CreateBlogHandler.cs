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
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Busines.Cqrs.Handlers
{
    public class CreateBlogHandler : IRequestHandler<CreateBlogCommand, Response<BlogCreateDTO>>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IBlogRepository _blogRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateBlogHandler(AppDbContext context, IMapper mapper,IBlogRepository blogRepository,IUnitOfWork unitOfWork )
        {
            _context = context;
            _mapper = mapper;   
            _blogRepository = blogRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<BlogCreateDTO>> Handle(CreateBlogCommand request, CancellationToken cancellationToken)
        {
           
                var result = await new BlogCreateValidator().ValidateAsync(request);

                if (!result.IsValid)
                {
                    throw new ValidationException(result.Errors);
                }


                var entity = _mapper.Map<Blog>(request);


            //List<BlogTag> blogTags = new List<BlogTag>();

            //foreach (int tagId in entity.TagIds)
            //{
            //    if (entity.TagIds.Where(t=> t == tagId).Count()>1 )
            //    {
            //        throw new ValidationException("bit tagdan yalniz bir defe secile biler");
            //    }

            //    if (!await _context.Tags.AnyAsync(t=> t.Id == tagId))
            //    {
            //        throw new ValidationException("duzgun tag secin");
            //    }

            //    BlogTag blogTag = new BlogTag()
            //    {
            //        CreatedDate = DateTime.Now,

            //        TagId = tagId,
            //    };

            //    blogTags.Add(blogTag);
            //}
                
            //entity.BlogTags = blogTags;

                if (!await _context.Categories.AnyAsync(x => x.Id == entity.CategoryId))
                {
                    throw new ValidationException("gelen category yalnisdir");
                }


                await _blogRepository.CreateAsync(entity, cancellationToken);

                await _unitOfWork.CommitAsync(cancellationToken);

                var map = _mapper.Map<BlogCreateDTO>(entity);


                return new Response<BlogCreateDTO>
                {
                    Data = map,
                    Message = "create olundu"
                };
            
        

        }
    }
}
