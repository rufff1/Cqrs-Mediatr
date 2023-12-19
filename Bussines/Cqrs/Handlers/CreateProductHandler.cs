using AutoMapper;
using Busines.Cqrs.Commands;
using Busines.DTOs.Product.Request;
using Busines.Exceptions;
using Busines.Validators.Product;
using Business.DTOs.Category.Request;
using Bussines.DTOs.Common;
using Common.Entities;
using DataAccess.Context;
using DataAccess.Repositories.Abstract;
using DataAccess.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Busines.Cqrs.Handlers
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, Response<ProductCreateDTO>>
    {

        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public CreateProductHandler(AppDbContext context, IProductRepository productRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = context;
        }

        public async Task<Response<ProductCreateDTO>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {

            var result = await new ProductCreateValidator().ValidateAsync(request);

            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }

            var product = _mapper.Map<Product>(request);


            if (!await _context.Categories.AnyAsync(c => c.Id == product.CategoryId))
            {
               throw new ValidationException("gelen category yalnisdir");
            }

            await _context.AddAsync(product, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var map = _mapper.Map<ProductCreateDTO>(product);

            return new Response<ProductCreateDTO>
            {
                Data = map,
                Message = "product ugurla yaradildi"
            };

        }
    }
}
