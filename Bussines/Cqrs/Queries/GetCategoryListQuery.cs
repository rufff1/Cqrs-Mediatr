using Business.DTOs.Category.Response;
using Bussines.DTOs.Common;
using Common.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines.Cqrs.Queries
{
    public class GetCategoryListQuery : IRequest<Response<List<CategoryDTO>>>
    {
    }
}
