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
    public class GetTagListHandler : IRequestHandler<GetAllTagQuery, Response<List<TagDTO>>>
    {
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;

        public GetTagListHandler(ITagRepository tagRepository , IMapper mapper)
        {
             _mapper = mapper;
            _tagRepository = tagRepository;
        }

        public async Task<Response<List<TagDTO>>> Handle(GetAllTagQuery request, CancellationToken cancellationToken)
        {
            var result = await _tagRepository.GetAllAsync();

            if (result.Count() <= 0) { throw new NotFoundException("hec bir tag movcud deyil"); }

            return new Response<List<TagDTO>>
            {
                Data = _mapper.Map<List<TagDTO>>(result),
                Message = "taglar getirildi"
            };
        }
    }
}
