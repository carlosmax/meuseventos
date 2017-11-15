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
            var user = UserUtils.GerarRegisterViewModel();

            // Act 
            var userResult = await Environment.Client.CadastrarOrganizador(user);
            var token = userResult.data.result.access_token;

            // Assert
            Assert.True(userResult.success);
            Assert.NotEmpty(token);
        }
    }
}