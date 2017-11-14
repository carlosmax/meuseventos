using FluentValidation;
using MediatR;
using MeusEventos.Domain.Core.Common;
using MeusEventos.Domain.Core.Exceptions;
using MeusEventos.Domain.Organizadores.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MeusEventos.Domain.Organizadores.Features
{
    public class Registro
    {
        public class Command : IRequest<Guid>
        {
            public Command(Guid id, string nome, string cpf, string email)
            {
                Id = id;
                Nome = nome;
                CPF = cpf;
                Email = email;
            }

            public Guid Id { get; private set; }
            public string Nome { get; private set; }
            public string CPF { get; private set; }
            public string Email { get; private set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(i => i.Nome)
                    .NotEmpty()
                    .WithMessage("O nome é obrigatório");

                RuleFor(i => i.CPF)
                    .NotEmpty()
                    .WithMessage("O CPF é obrigatório");

                RuleFor(i => i.CPF)
                    .Matches(RegularExpression.CPF)
                    .When(i => !string.IsNullOrEmpty(i.CPF))
                    .WithMessage("O CPF informado é inválido");

                RuleFor(i => i.Email)
                    .EmailAddress()
                    .WithMessage("O email informado é inválido");
            }
        }

        public class Handler : IAsyncRequestHandler<Command, Guid>
        {
            private readonly IOrganizadorRepository _organizadorRepository;

            public Handler(IOrganizadorRepository organizadorRepository)
            {
                _organizadorRepository = organizadorRepository;
            }

            public async Task<Guid> Handle(Command message)
            {   
                var validator = new Validator();
                var validationResult = validator.Validate(message);

                if (!validationResult.IsValid)
                {
                    throw new ValidationResultException(validationResult);
                }

                var organizador = new Organizador(message.Id, message.Nome, message.CPF, message.Email);
                var organizadorExistente = _organizadorRepository.Buscar(o => o.CPF == organizador.CPF || o.Email == organizador.Email);

                if (organizadorExistente.Any())
                {
                    throw new InvalidOperationException("CPF ou e-mail já utilizados");
                }

                _organizadorRepository.Adicionar(organizador);
                await _organizadorRepository.SaveChangesAsync();

                return organizador.Id;
            }
        }
    }
}
