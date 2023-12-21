

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

namespace Busines.Cqrs.Handlers
{
    public class CreateTagHandler : IRequestHandler<CreateTagCommand, Response<TagCreateDTO>>
    {
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppDbContext _context;

        public CreateTagHandler(AppDbContext context,ITagRepository tagRepository,IMapper mapper, IUnitOfWork unitOfWork )
        {
            _mapper = mapper;
            _tagRepository = tagRepository;
            _unitOfWork = unitOfWork;
            _context = context;
        }


        public async Task<Response<TagCreateDTO>> Handle(CreateTagCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var result = await new TagCreateValidator().ValidateAsync(request);

                if (!result.IsValid)
                {
                    throw new ValidationException(result.Errors);
                }

                var tag = _mapper.Map<Tag>(request);

                if ((await _context.Tags.AnyAsync(x => x.Name.ToLower().Trim() == request.Name.ToLower().Trim())))
                { throw new ValidationException("Bu adla tag var"); }


                await _tagRepository.CreateAsync(tag, cancellationToken);
                await _unitOfWork.CommitAsync(cancellationToken);


                  var map = _mapper.Map<TagCreateDTO>(tag);
                return new Response<TagCreateDTO>
                {
                    Data = map,
                    Message = "tag ugurla yaradildi"
                };
            }
            catch (Exception e)
            {

                int a = 32;
            }

            return null;
        }
    }
}
