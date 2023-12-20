using Busines.Cqrs.Commands;
using Busines.Cqrs.Queries;
using Busines.DTOs.Blog.Request;
using Busines.DTOs.Blog.Response;
using Business.DTOs.Category.Response;
using Bussines.DTOs.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BlogController(IMediator mediator)
        {
            _mediator = mediator;   
        }




        #region Documentation
        /// <summary>
        /// blog yaradılması üçün
        /// </summary>
        /// <param name="model"></param>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response))]
        #endregion
        [HttpPost("Create")]
        public async Task<Response<BlogCreateDTO>> CreateAsync([FromBody]CreateBlogCommand model)
        {
           return await _mediator.Send(model);
        }




        #region Documentation
        /// <summary>
        /// blog redaktə olunması üçün
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response))]
        #endregion
        [HttpPut("Update")]
        public async Task<Response<BlogUpdateDTO>> UpdateAsync([FromBody]UpdateBlogCommand model)
        {
            return await _mediator.Send(model);
        }


        #region Documentation
        /// <summary>
        ///  blog silinməsi
        /// </summary>
        /// <param name="id"></param>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response))]
        #endregion
        [HttpDelete("deleteById")]
        public async Task<Response> DeleteAsync([FromBody] UpdateBlogCommand model)
        {
            return await _mediator.Send(model);
        }



        #region Documentation
        /// <summary>
        ///  blog id parametrinə görə götürülməsi üçün
        /// </summary>
        /// <param name="id"></param>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<BlogDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response))]
        #endregion
        [HttpGet("GetById")]
        public async Task<Response<BlogDTO>> GetAsync(int id)
        {
            return await _mediator.Send(new GetBlogByIdQuery() { Id = id });
        }




        #region Documentation
        /// <summary>
        /// blog siyahısını götürmək üçün
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<List<BlogDTO>>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response))]
        #endregion
        [HttpGet("GetAll")]
        public async Task<Response<List<BlogDTO>>> GetAllAsync()
        {
            return await _mediator.Send(new GetBlogListQuery());
        }

    }
}
