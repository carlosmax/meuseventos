using MediatR;
using MeusEventos.Domain.Core.Exceptions;
using MeusEventos.Domain.Eventos.Repositories;
using System;
using System.Threading.Tasks;

namespace MeusEventos.Domain.Eventos.Features
{
    public class Registro
    {
        public class Command : EventoCommandBase, IRequest<Guid> { }

        public class Validator : ValidatorBase<Command> { }

        public class Handler : IAsyncRequestHandler<Command, Guid>
        {
            private readonly IEventoRepository _eventoRepository;

            public Handler(IEventoRepository eventoRepository)
            {
                _eventoRepository = eventoRepository;
            }

            public async Task<Guid> Handle(Command message)
            {
                var validator = new Validator();
                var validationResult = validator.Validate(message);

                if (!validationResult.IsValid)
                    throw new ValidationResultException(validationResult);

                var endereco = new Endereco(message.Endereco.Id, message.Endereco.Logradouro, message.Endereco.Numero, message.Endereco.Complemento, message.Endereco.Bairro, message.Endereco.CEP, message.Endereco.Cidade, message.Endereco.Estado, message.Endereco.EventoId.Value);

                var evento = Evento.EventoFactory.NovoEventoCompleto(message.Id, message.Nome, message.DescricaoCurta,
                    message.DescricaoLonga, message.DataInicio, message.DataFim, message.Gratuito, message.Valor,
                    message.Online, message.NomeEmpresa, message.OrganizadorId, endereco, message.CategoriaId);

                _eventoRepository.Adicionar(evento);
                await _eventoRepository.SaveChangesAsync();

                return evento.Id;
            }
        }
    }
}
