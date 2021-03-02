using EightCare.API.Constants;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using EightCare.Application.Keeper.Commands.RegisterKeeper;
using EightCare.Application.Keeper.Queries.GetKeeperById;

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
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> RegisterKeeper([FromBody] RegisterKeeperCommand registerKeeperCommand)
        {
            var createdKeeperId = await _mediator.Send(registerKeeperCommand);

            return Created(Routes.KeeperRoute + $"/{createdKeeperId}", new { id = createdKeeperId });
        }

        [HttpGet]
        [Route("{keeperId}")]
        [ProducesResponseType(typeof(KeeperDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetKeeperById([FromRoute] Guid keeperId)
        {
            var keeperModel = await _mediator.Send(new GetKeeperByIdQuery(keeperId));

            return Ok(keeperModel);
        }
    }
}
