using MeusEventos.Domain.Core.Common;
using System;
using System.Collections.Generic;

namespace MeusEventos.Domain.Eventos
{
    public class Categoria : Entity
    {
        public Categoria(Guid id)
        {
            Id = id;
        }

        public string Nome { get; private set; }

        // EF Propriedade de Navegação
        public virtual ICollection<Evento> Eventos { get; set; }

        // Construtor para o EF
        protected Categoria() { }
    }
}