using System.ComponentModel.DataAnnotations;

namespace MeusEventos.Infra.Identity.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
