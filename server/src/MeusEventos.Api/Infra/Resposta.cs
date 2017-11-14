using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace MeusEventos.Api.Infra
{
    public class Resposta
    {
        public Resposta()
        {
            Erros = new List<string>();
        }

        public Resposta(ValidationResult validationResult)
        {
            Erros = validationResult.Errors.Select(i => i.ErrorMessage).ToList();
            Sucesso = !Erros.Any();
        }

        public Resposta(ModelStateDictionary modelState)
        {
            Erros = modelState.Values.SelectMany(v => v.Errors).Select(i => i.ErrorMessage).ToList();
            Sucesso = !Erros.Any();
        }

        public Resposta(bool sucesso, string mensagem)
        {
            Sucesso = sucesso;
            Erros = new List<string>() { mensagem };
        }

        public Resposta(bool sucesso, object resultado)
        {
            Sucesso = sucesso;
            Resultado = resultado;
        }

        public bool Sucesso { get; set; }
        public object Resultado { get; set; }
        public List<string> Erros { get; set; }
    }
}
