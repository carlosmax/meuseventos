namespace MeusEventos.Domain.Core.Common
{
    public static class RegularExpression
    {
        public static string CPF => @"^\d{3}\.\d{3}\.\d{3}-\d{2}|\d{11}$";
    }
}
