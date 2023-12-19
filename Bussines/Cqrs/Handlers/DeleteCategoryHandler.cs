using Busines.Cqrs.Commands;
using Busines.Exceptions;
using Business.DTOs.Category.Request;
using Bussines.DTOs.Common;
using DataAccess.Context;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Bussines.Cqrs.Handlers
{
    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, Response>
    {
        public readonly ICategoryRepository _categoryRepository;
        public readonly AppDbContext _context;

        public DeleteCategoryHandler(AppDbContext context,ICategoryRepository categoryRepository)
        {
                _categoryRepository = categoryRepository;
            _context = context;
        }

        public async Task<Response> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var result = await _categoryRepository.GetAsync(request.Id);
            if (result == null)
                throw new NotFoundException("category tapilmadi");


             _categoryRepository.Delete(result);

             await _context.SaveChangesAsync(cancellationToken);

            return new Response
            {
                Message = "category ugurla silindi"
            };

        }
    }
}
