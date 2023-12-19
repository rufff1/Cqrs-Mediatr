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
using DataAccess.Repositories.Concrete;
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
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, Response<ProductUpdateDTO>>
    {

        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public UpdateProductHandler(AppDbContext context, IProductRepository productRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = context;
        }

        public async Task<Response<ProductUpdateDTO>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var result = await new ProductUpdateValidator().ValidateAsync(request);
            if (!result.IsValid)
            {
                

                throw new ValidationException(result.Errors);
            }

            var existProduct = await _productRepository.GetAsync(request.Id);
            if (existProduct is null)
            {
                throw new NotFoundException("Product tapılmadı");
            }


            if (!await _context.Categories.AnyAsync(t => t.Id == existProduct.CategoryId))
            {
                throw new ValidationException("duzgun category secin");
            }



            var model = _mapper.Map<Product>(request);
             existProduct.ModifiedDate = DateTime.Now;
             existProduct.CategoryId = request.CategoryId;
            existProduct.Name = request.Name;
            existProduct.Description = request.Description;
              _productRepository.Update(existProduct);
            await _context.SaveChangesAsync(cancellationToken);

            var map = _mapper.Map<ProductUpdateDTO>(model);

            return new Response<ProductUpdateDTO>
            {
                Data = map,
                Message = "ugurla update olundu"
            };
        }
    }
}
