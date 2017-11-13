using FluentValidation.Results;
using System;
using System.Runtime.Serialization;

namespace MeusEventos.Domain.Core.Exceptions
{
    public class ValidationResultException : Exception
    {
        public ValidationResult ValidationResult { get; }

        public ValidationResultException()
        {
        }

        public ValidationResultException(string message) : base(message)
        {
        }

        public ValidationResultException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ValidationResultException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public ValidationResultException(ValidationResult validationResult) : base("Verifique o ValidationResult para mais detalhes do erro")
        {
            ValidationResult = validationResult;
        }
    }
}
