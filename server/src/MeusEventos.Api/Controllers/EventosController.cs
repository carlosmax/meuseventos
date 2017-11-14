using CMax.Api.Versioning;
using MediatR;
using MeusEventos.Domain.Core.Common;
using MeusEventos.Domain.Eventos.Features;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MeusEventos.Api.Controllers
{
    [VersionedRoute("api/eventos")]
    public class EventosController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public EventosController(IUser user, IMediator mediator) : base(user)
        {
            _mediator = mediator;
        }

        [HttpPost]        
        //[Authorize(Policy = "PodeGravar")]
        public async Task<IActionResult> Post([FromBody]Registro.Command command)
        {
            await _mediator.Send(command);

            return Sucesso();
        }
    }
}