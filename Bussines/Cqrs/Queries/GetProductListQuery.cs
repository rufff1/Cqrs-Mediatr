using Busines.DTOs.Product.Response;
using Bussines.DTOs.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Busines.Cqrs.Queries
{
    public class GetProductListQuery : IRequest<Response<List<ProductDTO>>>
    {
    }
}
