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
    public class DeleteTagHandler : IRequestHandler<DeleteTagCommand, Response>
    {
        private readonly ITagRepository _tagRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTagHandler(ITagRepository tagRepository, IUnitOfWork unitOfWork)
        {
            _tagRepository = tagRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
        {
            var result = await _tagRepository.GetAsync(request.Id);

            if (result == null) { throw new NotFoundException("tag tapilmadi"); }

            _tagRepository.Delete(result);

           await _unitOfWork.CommitAsync(cancellationToken);

            return new Response
            {
                Message = "ugurla silindi"
            };

        }
    }
}
