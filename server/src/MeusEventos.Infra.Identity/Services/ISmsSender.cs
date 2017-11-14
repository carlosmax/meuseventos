using System.Threading.Tasks;

namespace MeusEventos.Infra.Identity.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
