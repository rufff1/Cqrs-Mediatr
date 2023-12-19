using AutoMapper;
using Busines.Exceptions;
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
    public class GetCategorytListHandler : IRequestHandler<GetCategoryListQuery, Response<List<CategoryDTO>>>
    {
        public readonly ICategoryRepository _categoryRepository;
        public readonly AppDbContext _context;
        public readonly IMapper _mapper;

        public GetCategorytListHandler(IMapper mapper, AppDbContext context, ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _context = context;
            _mapper = mapper;
        }
        public async Task<Response<List<CategoryDTO>>> Handle(GetCategoryListQuery request, CancellationToken cancellationToken)
        {
            var response = await _categoryRepository.GetAllAsync();

            if (response.Count <= 0 )
            {
                throw new NotFoundException("hec bir category yoxdur");
            }

           // var category = _mapper.Map<Category>(response);



            return new Response<List<CategoryDTO>>
            {
                Data = _mapper.Map<List<CategoryDTO>>(response),

                Message = "Tag tapildi"
            };

        }
    }
}
