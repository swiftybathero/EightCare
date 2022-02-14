using EightCare.API.Constants;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using EightCare.Application.Collections.Commands.DeleteCollection;
using EightCare.Application.Collections.Commands.RegisterCollection;
using EightCare.Application.Collections.Queries.GetCollectionById;

namespace EightCare.API.Controllers
{
    [ApiController]
    [Route(Routes.CollectionRoute)]
    public class CollectionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CollectionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> RegisterCollection([FromBody] RegisterCollectionCommand registerCollectionCommand)
        {
            var createdCollectionId = await _mediator.Send(registerCollectionCommand);

            return Created(Routes.CollectionRoute + $"/{createdCollectionId}", new { Id = createdCollectionId });
        }

        [HttpGet]
        [Route("{collectionId}")]
        [ProducesResponseType(typeof(CollectionDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCollectionById([FromRoute] Guid collectionId)
        {
            var collectionModel = await _mediator.Send(new GetCollectionByIdQuery(collectionId));

            if (collectionModel is null)
            {
                return NotFound();
            }

            return Ok(collectionModel);
        }

        [HttpDelete]
        [Route("{collectionId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteCollection([FromRoute] Guid collectionId)
        {
            await _mediator.Send(new DeleteCollectionCommand(collectionId));

            return Ok();
        }
    }
}
