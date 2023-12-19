using Business.DTOs.Category.Request;
using Bussines.DTOs.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Busines.Cqrs.Commands
{
    public class UpdateCategoryCommand : IRequest<Response<CategoryUpdateDTO>>
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
