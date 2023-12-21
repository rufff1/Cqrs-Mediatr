
using AutoMapper;
using Busines.Cqrs.Commands;
using Busines.DTOs.Tag.Request;
using Busines.Exceptions;
using Busines.Validators.Tag;
using Bussines.DTOs.Common;
using Common.Entities;
using DataAccess.Context;
using DataAccess.Repositories.Abstract;
using DataAccess.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Busines.Cqrs.Handlers
{
    public class UpdateTagHandler : IRequestHandler<UpdateTagCommand, Response<TagUpdateDTO>>
    {
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppDbContext _context;



        public UpdateTagHandler(ITagRepository tagRepository, IMapper mapper, IUnitOfWork unitOfWork,AppDbContext context)
        {
            _mapper = mapper;
            _tagRepository = tagRepository;
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public async Task<Response<TagUpdateDTO>> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
        {
            var result = await new TagUpdateValidator().ValidateAsync(request);

            if (!result.IsValid) 
            { throw new ValidationException(result.Errors); }


            var existTag = await _tagRepository.GetAsync(request.Id);
       

            if (existTag == null) 
            { throw new NotFoundException("tag tapilmadi"); }


            var entity = _mapper.Map(request,existTag);


            if (await _context.Tags.AnyAsync(t=> t.Name.ToLower().Trim() == entity.Name.ToLower().Trim())) 
            { throw new ValidationException("bu tag artig movcuddur"); }


          
            existTag.ModifiedDate = DateTime.UtcNow;

            
             _tagRepository.Update(existTag);
          
             await _unitOfWork.CommitAsync(cancellationToken);
            
            
           var map = _mapper.Map<TagUpdateDTO>(entity);

            return new Response<TagUpdateDTO>
            {
                Data = map,
                Message = "update olundu"
            };
        }
    }
}
