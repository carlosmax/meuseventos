using MeusEventos.Domain.Core.Common;
using MeusEventos.Domain.Eventos;
using System;
using System.Collections.Generic;

namespace MeusEventos.Domain.Organizadores
{
    public class Organizador : AggregateRoot
    {
        protected Organizador() { }

        public Organizador(Guid id, string nome, string cpf, string email)
        {
            Id = id;
            Nome = nome;
            CPF = cpf;
            Email = email;
        }

        public string Nome { get; private set; }
        public string CPF { get; private set; }
        public string Email { get; private set; }

        public virtual ICollection<Evento> Eventos { get; set; }
    }
}