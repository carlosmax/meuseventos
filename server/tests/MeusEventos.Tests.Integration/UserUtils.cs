using Bogus;
using Bogus.DataSets;
using Bogus.Extensions.Brazil;
using MeusEventos.Infra.Identity.Models.AccountViewModels;
using MeusEventos.Tests.Integration.DTO;
using Newtonsoft.Json;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MeusEventos.Tests.Integration
{
    public static class UserUtils
    {
        public static RegisterViewModel GerarRegisterViewModel()
        {
            // Arrange 
            var userFaker = new Faker<RegisterViewModel>("pt_BR")
                .RuleFor(r => r.Nome, c => c.Name.FullName(Name.Gender.Male))
                .RuleFor(r => r.CPF, c => c.Person.Cpf().Replace(".", "").Replace("-", ""))
                // remoção de acento no email
                .RuleFor(r => r.Email, (c, r) => RemoverAcentos(c.Internet.Email(r.Nome).ToLower()));

            var user = userFaker.Generate();
            user.Senha = "Teste@123";
            user.SenhaConfirmacao = "Teste@123";

            return user;
        }

        public static async Task<RegistroUsuarioJson> CadastrarOrganizador(this HttpClient client, RegisterViewModel model)
        {
            var postContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("api/nova-conta", postContent);
            var userResult = JsonConvert.DeserializeObject<RegistroUsuarioJson>(await response.Content.ReadAsStringAsync());

            return userResult;
        }

        public static async Task<Result> RealizarLoginOrganizador(this HttpClient client, string email, string senha)
        {
            var user = new LoginViewModel
            {
                Email = email,
                Senha = senha
            };

            var postContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("api/conta", postContent);

            var postResult = await response.Content.ReadAsStringAsync();
            var userResult = JsonConvert.DeserializeObject<RegistroUsuarioJson>(postResult);

            return userResult.data.result;
        }

        private static string RemoverAcentos(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
