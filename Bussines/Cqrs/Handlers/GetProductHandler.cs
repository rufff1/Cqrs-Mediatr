using AutoMapper;
using Busines.Cqrs.Queries;
using Busines.DTOs.Product.Response;
using Busines.Exceptions;
using Bussines.DTOs.Common;
using DataAccess.Context;
using DataAccess.Repositories.Abstract;
using DataAccess.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Busines.Cqrs.Handlers
{
    public class GetProductHandler : IRequestHandler<GetProductByIdQuery, Response<ProductDTO>>
    {
        private readonly IProductRepository _productRepository;
     
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public GetProductHandler(AppDbContext context, IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _context = context;
        }
        public async Task<Response<ProductDTO>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _productRepository.GetAsync(request.Id);

            if (result == null) throw new NotFoundException("product tapilmadi");


            return new Response<ProductDTO>
            {
               Data = _mapper.Map<ProductDTO>(result),
               Message = "product tapildi"
            };

        }
    }
}
