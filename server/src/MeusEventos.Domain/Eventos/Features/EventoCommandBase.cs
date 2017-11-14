using FluentValidation;
using System;

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
        public EnderecoCommand Endereco { get; set; }
    }

    public class EnderecoCommand
    {
        public Guid Id { get; private set; }
        public string Logradouro { get; private set; }
        public string Numero { get; private set; }
        public string Complemento { get; private set; }
        public string Bairro { get; private set; }
        public string CEP { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }
        public Guid? EventoId { get; private set; }
    }

    public abstract class ValidatorBase<T> : AbstractValidator<T> where T: EventoCommandBase
    {
        public ValidatorBase()
        {
            ValidarNome();
            ValidarValor();
            ValidarData();
            ValidarLocal();
            ValidarNomeEmpresa();
            ValidarEndereco();
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

        private void ValidarEndereco()
        {
            RuleFor(c => c.Endereco.Logradouro)
                .NotEmpty()
                .When(c => !c.Online)
                .WithMessage("O Logradouro precisa ser fornecido")
                .Length(2, 150)
                .When(c => !c.Online)
                .WithMessage("O Logradouro precisa ter entre 2 e 150 caracteres");

            RuleFor(c => c.Endereco.Bairro)
                .NotEmpty()
                .When(c => !c.Online)
                .WithMessage("O Bairro precisa ser fornecido")
                .Length(2, 150)
                .When(c => !c.Online)
                .WithMessage("O Bairro precisa ter entre 2 e 150 caracteres");

            RuleFor(c => c.Endereco.CEP)
                .NotEmpty()
                .When(c => !c.Online)
                .WithMessage("O CEP precisa ser fornecido")
                .Length(8)
                .When(c => !c.Online)
                .WithMessage("O CEP precisa ter 8 caracteres");

            RuleFor(c => c.Endereco.Cidade)
                .NotEmpty()
                .When(c => !c.Online)
                .WithMessage("A Cidade precisa ser fornecida")
                .Length(2, 150)
                .When(c => !c.Online)
                .WithMessage("A Cidade precisa ter entre 2 e 150 caracteres");

            RuleFor(c => c.Endereco.Estado)
                .NotEmpty()
                .When(c => !c.Online)
                .WithMessage("O Estado precisa ser fornecido")
                .Length(2, 150)
                .When(c => !c.Online)
                .WithMessage("O Estado precisa ter entre 2 e 150 caracteres");

            RuleFor(c => c.Endereco.Numero)
                .NotEmpty()
                .When(c => !c.Online)
                .WithMessage("O Numero precisa ser fornecido")
                .Length(1, 10)
                .When(c => !c.Online)
                .WithMessage("O Numero precisa ter entre 1 e 10 caracteres");
        }
    }
}
