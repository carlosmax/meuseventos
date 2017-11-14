using Bogus;
using Bogus.DataSets;
using MeusEventos.Infra.Identity.Models.AccountViewModels;
using Newtonsoft.Json;
using Bogus.Extensions.Brazil;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using MeusEventos.Tests.Integration.DTO;

namespace MeusEventos.Tests.Integration
{
    public class ContaControllerTests
    {
        public ContaControllerTests()
        {
            Environment.CreateServer();
        }

        [Fact(DisplayName = "Registrar organizador com sucesso")]
        [Trait("Category", "Testes de integração da API")]
        public async Task ContaController_RegistrarNovoOrganizador_RetornarComSucesso()
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

            var postContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            // Act 
            var response = await Environment.Client.PostAsync("api/nova-conta", postContent);

            var userResult = JsonConvert.DeserializeObject<RegistroContaResponse>(await response.Content.ReadAsStringAsync());
            var token = userResult.resultado.result.access_token;

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.NotEmpty(token);
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