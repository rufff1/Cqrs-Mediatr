using Busines.DTOs.Tag.Response;
using Bussines.DTOs.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Busines.Cqrs.Queries
{
    public class GetTagByIdQuery :IRequest<Response<TagDTO>>
    {
        public int Id { get; set; }
    }
}
