
using AutoMapper;
using Busines.Cqrs.Queries;
using Busines.DTOs.Tag.Response;
using Busines.Exceptions;
using Bussines.DTOs.Common;
using DataAccess.Repositories.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Busines.Cqrs.Handlers
{
    public class GetTagHandler : IRequestHandler<GetTagByIdQuery, Response<TagDTO>>
    {
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;

        public GetTagHandler(ITagRepository tagRepository,IMapper mapper)
        {
            _mapper = mapper;
            _tagRepository = tagRepository;
        }


        public async Task<Response<TagDTO>> Handle(GetTagByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _tagRepository.GetAsync(request.Id);

             if (result == null) { throw new NotFoundException("tag tapilmadi"); }

            return new Response<TagDTO>
            {
                Data = _mapper.Map<TagDTO>(result),
                Message = "tag tapildi"
            };
        }
    }
}
