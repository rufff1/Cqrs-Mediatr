using Busines.Cqrs.Commands;
using Busines.Cqrs.Queries;
using Busines.DTOs.Blog.Response;
using Busines.DTOs.Tag.Request;
using Busines.DTOs.Tag.Response;
using Business.DTOs.Category.Response;
using Bussines.DTOs.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TagController(IMediator mediator)
        {
            _mediator = mediator;   
        }



        #region Documentation
        /// <summary>
        /// tag yaradılması üçün
        /// </summary>
        /// <param name="model"></param>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response))]
        #endregion
        [HttpPost("Create")]
        public async Task<Response<TagCreateDTO>> CreateAsync([FromBody]CreateTagCommand model)
        {
            return await _mediator.Send(model);
        }



        #region Documentation
        /// <summary>
        /// tag redaktə olunması üçün
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response))]
        #endregion
        [HttpPut("Update")]
        public async Task<Response<TagUpdateDTO>> UpdateAsync([FromBody] UpdateTagCommand model)
        {
            return await _mediator.Send(model);
        }



        #region Documentation
        /// <summary>
        ///  tag silinməsi
        /// </summary>
        /// <param name="id"></param>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response))]
        #endregion
        [HttpDelete("Id")]
        public async Task<Response> DeleteAsync([FromBody] DeleteTagCommand model)
        {
            return await _mediator.Send(model);
        }


        #region Documentation
        /// <summary>
        ///  tag id parametrinə görə götürülməsi üçün
        /// </summary>
        /// <param name="id"></param>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<TagDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response))]
        #endregion
        [HttpGet("TagById")]
        public async Task<Response<TagDTO>> GetAsync(int id)
        {
            return await _mediator.Send(new GetTagByIdQuery() { Id = id});
        }



        #region Documentation
        /// <summary>
        /// tag siyahısını götürmək üçün
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<List<TagDTO>>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response))]
        #endregion
        [HttpGet("AllTag")]
        public async Task<Response<List<TagDTO>>> GetAllAsync()
        {
            return await _mediator.Send(new GetAllTagQuery());
        }
    }
}
