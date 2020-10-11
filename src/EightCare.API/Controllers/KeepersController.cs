using EightCare.API.Commands;
using EightCare.API.Constants;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EightCare.API.Controllers
{
    [ApiController]
    [Route(Routes.KeeperRoute)]
    public class KeepersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public KeepersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult RegisterKeeper([FromBody] RegisterKeeperCommand command)
        {
            var createdKeeperId = _mediator.Send(command);

            return Created(Routes.KeeperRoute + $"/{createdKeeperId}", new { id = createdKeeperId });
        }
    }
}