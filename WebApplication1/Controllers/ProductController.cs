using Busines.Cqrs.Commands;
using Busines.Cqrs.Queries;
using Busines.DTOs.Product.Request;
using Busines.DTOs.Product.Response;
using Business.DTOs.Category.Request;
using Business.DTOs.Category.Response;
using Bussines.Cqrs.Queries;
using Bussines.DTOs.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }



        #region Documentation
        /// <summary>
        /// product yaradılması üçün
        /// </summary>
        /// <param name="model"></param>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response))]
        #endregion
        [HttpPost("Create")]
        public async Task<Response<ProductCreateDTO>> CreateAsync([FromBody] CreateProductCommand model)
        {
            return await _mediator.Send(model);

        }



        #region Documentation
        /// <summary>
        /// Product redaktə olunması üçün
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response))]
        #endregion
        [HttpPut("Update")]
        public async Task<Response<ProductUpdateDTO>> UpdateAsync([FromBody] UpdateProductCommand model)
        {
            return await _mediator.Send(model);

        }

        #region Documentation
        /// <summary>
        ///  Product silinməsi
        /// </summary>
        /// <param name="id"></param>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response))]
        #endregion
        [HttpDelete("Delete")]
        public async Task<Response> DeleteAsync([FromBody] DeleteProductCommand model)
        {
            return await _mediator.Send(model);
        }

        #region Documentation
        /// <summary>
        ///  Product id parametrinə görə götürülməsi üçün
        /// </summary>
        /// <param name="id"></param>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<ProductDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response))]
        #endregion
        [HttpGet("ProductId")]
        public async Task<Response<ProductDTO>> GetByIdAsync(int id)
        {
            return await _mediator.Send(new GetProductByIdQuery() { Id = id });


        }




    }
}
