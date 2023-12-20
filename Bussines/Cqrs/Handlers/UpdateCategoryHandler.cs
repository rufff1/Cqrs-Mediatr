using AutoMapper;
using Busines.Cqrs.Commands;
using Busines.Exceptions;
using Busines.Validators.Category;
using Business.DTOs.Category.Request;
using Bussines.DTOs.Common;
using Common.Entities;
using DataAccess.Context;
using DataAccess.Repositories.Abstract;
using DataAccess.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines.Cqrs.Handlers
{
    public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, Response<CategoryUpdateDTO>>
    {

        private readonly ICategoryRepository _categoryRepository;
      
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public UpdateCategoryHandler(AppDbContext context, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
             _mapper = mapper;
            _context = context;
        }
        public async Task<Response<CategoryUpdateDTO>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var result = await new CategoryUpdateValidator().ValidateAsync(request);

            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }


            var category = await _categoryRepository.GetAsync(request.Id);


            if (category == null)
            {
               
                throw new NotFoundException("tag tapilmadi");
            }
            


            var model = _mapper.Map<Category>(request);


            if ((await _context.Categories.AnyAsync(x=> x.Name.ToLower().Trim() == model.Name.ToLower().Trim())))
            { throw new ValidationException("bu adla category movcuddur"); }


            category.ModifiedDate = DateTime.Now;

            _categoryRepository.Update(category);
            await _context.SaveChangesAsync(cancellationToken);

            var map = _mapper.Map<CategoryUpdateDTO>(model);

            return new Response<CategoryUpdateDTO>
            {
                Data = map,
                Message = "ugurla update olundu"
            };
            
        }
    }
}
