﻿using Business.DTOs.Category.Response;
using Bussines.DTOs.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines.Cqrs.Queries
{
    public class GetCategoryByIdQuery : IRequest<Response<CategoryDTO>>
    {
        public int Id { get; set; }
    }
}
