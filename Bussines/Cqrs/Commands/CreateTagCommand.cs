﻿using Busines.DTOs.Tag.Request;
using Bussines.DTOs.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Busines.Cqrs.Commands
{
    public class CreateTagCommand :IRequest<Response<TagCreateDTO>>
    {
        public string Name { get; set; }

    }
}
