using MeusEventos.Api.Infra;
using MeusEventos.Domain.Core.Common;
using MeusEventos.Domain.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System;

namespace MeusEventos.Api.Controllers
{
    [Produces("application/json")]
    public class ControllerBase : Controller
    {
        protected Guid OrganizadorId { get; set; }

        public ControllerBase(IUser user)
        {
            if (user.IsAuthenticated())
            {
                OrganizadorId = user.GetUserId();
            }
        }

        protected IActionResult Sucesso(object result = null, bool objCreated = false, string url = "")
        {
            var response = new Response(true, result);
            return objCreated ? (IActionResult)Created(url, response) : Ok(response);
        }

        protected IActionResult Falha(object data)
        {
            var response = new Response(false, data);
            return BadRequest(response);
        }

        protected IActionResult Falha(ValidationResultException result)
        {
            var response = new Response(result.ValidationResult);
            return BadRequest(response);
        }
    }
}