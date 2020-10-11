using EightCare.API.Commands;
using EightCare.API.Constants;
using EightCare.API.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        [ProducesDefaultResponseType]
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
    }
}