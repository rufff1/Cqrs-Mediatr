using AutoMapper;
using Busines.Exceptions;
using Business.DTOs.Category.Request;
using Business.DTOs.Category.Response;
using Bussines.Cqrs.Queries;
using Bussines.DTOs.Common;
using Common.Entities;
using DataAccess.Context;
using DataAccess.Repositories.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines.Cqrs.Handlers
{
    public class GetCategoryHandler : IRequestHandler<GetCategoryByIdQuery,Response<CategoryDTO>>
    {
        public readonly ICategoryRepository _categoryRepository;
        public readonly IMapper _mapper;

        public GetCategoryHandler(IMapper mapper, ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<Response<CategoryDTO>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var response = await _categoryRepository.GetAsync(request.Id);

            if (response == null)
            {
                
                throw new NotFoundException("category Tapilmadi");

            }
             // var category = _mapper.Map<Category>(response);



            return new Response<CategoryDTO>
            {
                Data = _mapper.Map<CategoryDTO>(response),

                Message = "Tag tapildi"
            };


        }
    }
}
