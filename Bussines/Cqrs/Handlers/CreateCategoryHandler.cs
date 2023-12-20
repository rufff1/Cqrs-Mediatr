

using AutoMapper;
using Busines.Cqrs.Commands;
using Busines.Exceptions;
using Business.DTOs.Category.Request;
using Busines.Validators.Category;
using Common.Entities;
using DataAccess.Repositories.Abstract;
using DataAccess.UnitOfWork;
using MediatR;
using DataAccess.Context;
using Bussines.DTOs.Common;
using Microsoft.EntityFrameworkCore;

namespace Bussines.Cqrs.Handlers
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, Response<CategoryCreateDTO>>
    {

        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public CreateCategoryHandler(AppDbContext context,ICategoryRepository categoryRepository,IUnitOfWork unitOfWork,IMapper mapper)
        {
            _categoryRepository = categoryRepository;
             _unitOfWork = unitOfWork;  
            _mapper = mapper;
            _context = context;
        }

     

        public async Task<Response<CategoryCreateDTO>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {

            var result = await new CategoryCreateValidator().ValidateAsync(request);

            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }

                var category = _mapper.Map<Category>(request);

            if ((await _context.Categories.AnyAsync(x=> x.Name.ToLower().Trim() == request.Name.ToLower().Trim())))
            { throw new ValidationException("Bu adla category var"); }

            //await _categoryRepository.CreateAsync(category);

           // await _unitOfWork.CommitAsync(cancellationToken);

            await _context.AddAsync(category, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
                  
            var map = _mapper.Map<CategoryCreateDTO>(category);

            return new Response<CategoryCreateDTO>
            {
                Data = map,
                Message = "category ugurla yaradildi"
            };
          
        }






    }
}
