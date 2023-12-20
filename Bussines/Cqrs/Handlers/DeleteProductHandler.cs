using Busines.Cqrs.Commands;
using Busines.Exceptions;
using Bussines.DTOs.Common;
using Common.Entities;
using DataAccess.Context;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Concrete;
using DataAccess.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Busines.Cqrs.Handlers
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, Response>
    {
        public readonly IProductRepository _productRepository;
        public readonly AppDbContext _context;
        public readonly IUnitOfWork _unitOfWork;

        public DeleteProductHandler(IUnitOfWork unitOfWork,AppDbContext context, IProductRepository productRepository)
        {
            _productRepository = productRepository;
            _context = context;
            _unitOfWork= unitOfWork;
        }

        public async Task<Response> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var result = await _productRepository.GetAsync(request.Id);
            if (result == null)
                throw new NotFoundException("product tapilmadi");


            _productRepository.Delete(result);

    
        
            await _unitOfWork.CommitAsync(cancellationToken);

            return new Response
            {
                Message = "product ugurla silindi"
            };
        }
    }
}
