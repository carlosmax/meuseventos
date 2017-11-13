using System;
using FluentValidation;
using MeusEventos.Domain.Core.Common;

namespace MeusEventos.Domain.Eventos
{
    public class Endereco : Entity
    {
        public string Logradouro { get; private set; }
        public string Numero { get; private set; }
        public string Complemento { get; private set; }
        public string Bairro { get; private set; }
        public string CEP { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }
        public Guid? EventoId { get; private set; }

        // EF Propriedade de Navegação
        public virtual Evento Evento { get; private set; }

        public Endereco(Guid id, string logradouro, string numero, string complemento, string bairro, string cep, string cidade, string estado, Guid? eventoId)
        {
            Id = id;
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            CEP = cep;
            Cidade = cidade;
            Estado = estado;
            EventoId = eventoId;
        }

        // Construtor para o EF
        protected Endereco() { }
    }
}