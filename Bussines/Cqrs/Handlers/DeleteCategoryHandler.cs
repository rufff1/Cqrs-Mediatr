using Busines.Cqrs.Commands;
using Busines.Exceptions;
using Bussines.DTOs.Common;
using Common.Entities;
using DataAccess.Context;
using DataAccess.Repositories.Abstract;
using DataAccess.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bussines.Cqrs.Handlers
{
    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, Response>
    {
        public readonly ICategoryRepository _categoryRepository;
        public readonly AppDbContext _context;
        public readonly IUnitOfWork _unitOfWork;

        public DeleteCategoryHandler(IUnitOfWork unitOfWork,AppDbContext context,ICategoryRepository categoryRepository)
        {
                _categoryRepository = categoryRepository;
            _context = context;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var result = await _context.Categories
                .Include(x=>  x.Blogs)
                .Include(x=> x.Products).FirstOrDefaultAsync(x=> x.Id == request.Id);
            if (result == null)
                throw new NotFoundException("category tapilmadi");


          
            if (result.Products.Count() > 0 || result.Blogs.Count() > 0)
            {
                throw new ValidationException("bu category bagli datalar var");
            }

          
            _categoryRepository.Delete(result);

          
            await _unitOfWork.CommitAsync(cancellationToken);

            return new Response
            {
                Message = "category ugurla silindi"
            };

        }
    }
}
