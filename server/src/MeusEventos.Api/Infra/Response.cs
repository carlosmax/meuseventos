using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace MeusEventos.Api.Infra
{
    public class Response
    {
        public Response()
        {
            Errors = new List<string>();
        }

        public Response(ValidationResult validationResult)
        {
            Errors = validationResult.Errors.Select(i => i.ErrorMessage).ToList();
            Success = !Errors.Any();
        }

        public Response(ModelStateDictionary modelState)
        {
            Errors = modelState.Values.SelectMany(v => v.Errors).Select(i => i.ErrorMessage).ToList();
            Success = !Errors.Any();
        }

        public Response(bool sucesso, string mensagem)
        {
            Success = sucesso;
            Errors = new List<string>() { mensagem };
        }

        public Response(bool sucesso, object resultado)
        {
            Success = sucesso;
            Data = resultado;
        }

        public bool Success { get; set; }
        public object Data { get; set; }
        public List<string> Errors { get; set; }
    }
}
