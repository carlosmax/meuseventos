﻿using System;

namespace MeusEventos.Domain.Eventos.Features
{
    public abstract class EventoCommandBase
    {
        public Guid Id { get; protected set; }
        public string Nome { get; protected set; }
        public string DescricaoCurta { get; protected set; }
        public string DescricaoLonga { get; protected set; }
        public DateTime DataInicio { get; protected set; }
        public DateTime DataFim { get; protected set; }
        public bool Gratuito { get; protected set; }
        public decimal Valor { get; protected set; }
        public bool Online { get; protected set; }
        public string NomeEmpresa { get; protected set; }
        public Guid OrganizadorId { get; protected set; }
        public Guid CategoriaId { get; protected set; }
    }
}