
using Busines.Cqrs.Commands;
using Busines.Exceptions;
using Bussines.DTOs.Common;
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
    public class DeleteBlogHandler : IRequestHandler<DeleteBlogCommand, Response>
    {
        public readonly IBlogRepository _blogRepository;
        public readonly IUnitOfWork _unitOfWork;


        public DeleteBlogHandler(IBlogRepository blogRepository,IUnitOfWork unitOfWork)
        {
            _blogRepository = blogRepository; 
            _unitOfWork = unitOfWork;
        }

        public async Task<Response> Handle(DeleteBlogCommand request, CancellationToken cancellationToken)
        {
            var result = await _blogRepository.GetAsync(request.Id);

            if (result == null)
            {
                throw new NotFoundException("blog tapilmadi");
            }


            _blogRepository.Delete(result);

            await _unitOfWork.CommitAsync(cancellationToken);


            return new Response
            {
                Message = "blog silindi"
            };


        }
    }
}
