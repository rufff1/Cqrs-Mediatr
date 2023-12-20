using AutoMapper;
using Busines.Cqrs.Queries;
using Busines.DTOs.Product.Response;
using Busines.Exceptions;
using Bussines.DTOs.Common;
using DataAccess.Context;
using DataAccess.Repositories.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Busines.Cqrs.Handlers
{
    public class GetProductListHandler : IRequestHandler<GetProductListQuery, Response<List<ProductDTO>>>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;


        public GetProductListHandler(AppDbContext context,IMapper mapper,IProductRepository productRepository)
        {
            _context = context;
            _mapper = mapper;
            _productRepository = productRepository;
        }


        public async Task<Response<List<ProductDTO>>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
        {
            var response = await _productRepository.GetAllAsync();

            if (response == null) { throw new NotFoundException("hec bir product tapilmadi"); }


            return new Response<List<ProductDTO>>
            {
                Data = _mapper.Map<List<ProductDTO>>(response),
                Message = "Productlar ugurla cekildi"
            };
        }
    }
}
