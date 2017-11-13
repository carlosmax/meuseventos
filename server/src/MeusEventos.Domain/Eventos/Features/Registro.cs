using FluentValidation;
using MediatR;
using MeusEventos.Domain.Core.Exceptions;
using MeusEventos.Domain.Eventos.Repositories;
using System;
using System.Threading.Tasks;

namespace MeusEventos.Domain.Eventos.Features
{
    public class Registro
    {
        public class Command : EventoCommandBase, IRequest<Guid>
        {
            public EnderecoCommand Endereco { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                ValidarNome();
                ValidarValor();
                ValidarData();
                ValidarLocal();
                ValidarNomeEmpresa();
            }

            private void ValidarNome()
            {
                RuleFor(c => c.Nome)
                    .NotEmpty().WithMessage("O nome do evento precisa ser fornecido")
                    .Length(2, 150).WithMessage("O nome do evento precisa ter entre 2 e 150 caracteres");
            }

            private void ValidarValor()
            {
                RuleFor(c => c.Valor)
                    .ExclusiveBetween(1, 50000)
                    .When(c => !c.Gratuito)
                    .WithMessage("O valor deve estar entre 1.00 e 50.000");

                RuleFor(c => c.Valor)
                    .Equal(0)
                    .When(e => e.Gratuito)
                    .WithMessage("O valor não deve diferente de 0 para um evento gratuito");
            }

            private void ValidarData()
            {
                RuleFor(c => c.DataInicio)
                    .LessThan(c => c.DataFim)
                    .WithMessage("A data de início deve ser maior que a data do final do evento");

                RuleFor(c => c.DataInicio)
                    .GreaterThan(DateTime.Now)
                    .WithMessage("A data de início não deve ser menor que a data atual");
            }

            private void ValidarLocal()
            {
                RuleFor(c => c.Endereco)
                    .Null()
                    .When(c => c.Online)
                    .WithMessage("O evento não deve possuir um endereço se for online");

                RuleFor(c => c.Endereco)
                    .NotNull()
                    .When(c => !c.Online)
                    .WithMessage("O evento deve possuir um endereço");

            }

            private void ValidarNomeEmpresa()
            {
                RuleFor(c => c.NomeEmpresa)
                    .NotEmpty().WithMessage("O nome do organizador precisa ser fornecido")
                    .Length(2, 150).WithMessage("O nome do organizador precisa ter entre 2 e 150 caracteres");
            }
        }

        public class Handler : IAsyncRequestHandler<Command, Guid>
        {
            private readonly IEventoRepository _eventoRepository;

            public Handler(IEventoRepository eventoRepository)
            {
                _eventoRepository = eventoRepository;
            }

            public async Task<Guid> Handle(Command message)
            {
                Validar(message);

                var endereco = new Endereco(message.Endereco.Id, message.Endereco.Logradouro, message.Endereco.Numero, message.Endereco.Complemento, message.Endereco.Bairro, message.Endereco.CEP, message.Endereco.Cidade, message.Endereco.Estado, message.Endereco.EventoId.Value);

                var evento = Evento.EventoFactory.NovoEventoCompleto(message.Id, message.Nome, message.DescricaoCurta,
                    message.DescricaoLonga, message.DataInicio, message.DataFim, message.Gratuito, message.Valor,
                    message.Online, message.NomeEmpresa, message.OrganizadorId, endereco, message.CategoriaId);

                _eventoRepository.Adicionar(evento);
                await _eventoRepository.SaveChangesAsync();

                return evento.Id;
            }

            private void Validar(Command message)
            {
                var validator = new Validator();
                var validationResult = validator.Validate(message);

                if (!validationResult.IsValid)
                    throw new ValidationResultException(validationResult);

                if (!message.Online)
                {
                    var validatorEndereco = new EnderecoValidator();
                    var validationResultEndereco = validatorEndereco.Validate(message.Endereco);

                    if (!validationResultEndereco.IsValid)
                        throw new ValidationResultException(validationResultEndereco);
                }
            }
        }
    }
}
