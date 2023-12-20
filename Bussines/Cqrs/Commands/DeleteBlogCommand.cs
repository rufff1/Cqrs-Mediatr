using Bussines.DTOs.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Busines.Cqrs.Commands
{
    public class DeleteBlogCommand : IRequest<Response>
    {
        public int Id { get; set; }

    }
}
