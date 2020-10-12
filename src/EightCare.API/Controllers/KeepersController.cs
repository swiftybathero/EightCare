using EightCare.API.Commands;
using EightCare.API.Constants;
using EightCare.API.Models;
using EightCare.API.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

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
        public async Task<IActionResult> RegisterKeeper([FromBody] RegisterKeeperModel registerKeeperModel)
        {
            var createdKeeperId = await _mediator.Send(new RegisterKeeperCommand
            (
                registerKeeperModel.Name,
                registerKeeperModel.Email,
                registerKeeperModel.Age
            ));

            return Created(Routes.KeeperRoute + $"/{createdKeeperId}", new { id = createdKeeperId });
        }

        [HttpGet]
        [Route("{keeperId}")]
        [ProducesResponseType(typeof(KeeperModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetKeeperById([FromRoute] Guid keeperId)
        {
            var keeperModel = await _mediator.Send(new GetKeeperByIdQuery(keeperId));

            return Ok(keeperModel);
        }
    }
}
