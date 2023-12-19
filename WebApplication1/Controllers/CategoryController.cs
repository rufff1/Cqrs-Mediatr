using Busines.Cqrs.Commands;
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
    public class CategoryController : ControllerBase
    {
        public readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }




        #region Documentation
        /// <summary>
        /// category yaradılması üçün
        /// </summary>
        /// <param name="model"></param>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response))]
        #endregion
        [HttpPost("Create")]
        public async Task<Response<CategoryCreateDTO>> CreateAsync([FromBody]CreateCategoryCommand model)
        {
            return await _mediator.Send(model);
   
        }





        #region Documentation
        /// <summary>
        /// category redaktə olunması üçün
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response))]
        #endregion
        [HttpPut("Update")]
        public async Task<Response<CategoryUpdateDTO>> UpdateAsync([FromBody] UpdateCategoryCommand model)
        {
            return await _mediator.Send(model);

        }




        #region Documentation
        /// <summary>
        ///  category silinməsi
        /// </summary>
        /// <param name="id"></param>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response))]
        #endregion
        [HttpDelete("Delete")]
        public async Task<Response> DeleteAsync ([FromBody] DeleteCategoryCommand model)
        {
            return await _mediator.Send(model);
        }





        #region Documentation
        /// <summary>
        ///  category id parametrinə görə götürülməsi üçün
        /// </summary>
        /// <param name="id"></param>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<CategoryDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response))]
        #endregion
        [HttpGet("CategoryId")]
        public async Task<Response<CategoryDTO>> GetByIdAsync(int id)
        {
            return await _mediator.Send(new GetCategoryByIdQuery() { Id = id });

        }



        #region Documentation
        /// <summary>
        /// category siyahısını götürmək üçün
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<List<CategoryDTO>>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response))]
        #endregion
        [HttpGet("CategoryAll")]
        public async Task<Response<List<CategoryDTO>>> GetAllStudentAsync()
        {

            return await _mediator.Send(new GetCategoryListQuery());
        }
    }
}
