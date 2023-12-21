using Busines.DTOs.Tag.Request;
using Bussines.DTOs.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Busines.Cqrs.Commands
{
    public class UpdateTagCommand : IRequest<Response<TagUpdateDTO>>
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
