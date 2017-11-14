using CMax.Api.Versioning;
using MediatR;
using MeusEventos.Domain.Core.Common;
using MeusEventos.Domain.Core.Exceptions;
using MeusEventos.Domain.Organizadores.Features;
using MeusEventos.Domain.Organizadores.Repositories;
using MeusEventos.Infra.Identity.Authorization;
using MeusEventos.Infra.Identity.Models;
using MeusEventos.Infra.Identity.Models.AccountViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MeusEventos.Api.Controllers
{
    public class ContaController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        private readonly IMediator _mediator;
        private readonly IOrganizadorRepository _organizadorRepository;
        private readonly TokenDescriptor _tokenDescriptor;
        
        public ContaController(
            IUser user, 
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager,
            ILoggerFactory loggerFactory,
            IMediator mediator,
            IOrganizadorRepository organizadorRepository,
            TokenDescriptor tokenDescriptor)
            : base(user)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = loggerFactory.CreateLogger<ContaController>();
            _mediator = mediator;
            _organizadorRepository = organizadorRepository;
            _tokenDescriptor = tokenDescriptor;
        }

        private static long ToUnixEpochDate(DateTime date)
      => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

        [HttpPost]
        [AllowAnonymous]
        [VersionedRoute("api/nova-conta")]
        public async Task<IActionResult> Registro([FromBody] RegisterViewModel model)
        {
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Senha);

            if (result.Succeeded)
            {
                await _userManager.AddClaimAsync(user, new Claim("Eventos", "Ler"));
                await _userManager.AddClaimAsync(user, new Claim("Eventos", "Gravar"));

                var registroCommand = new Registro.Command(Guid.Parse(user.Id), model.Nome, model.CPF, user.Email);
                Guid id;

                try
                {
                    id = await _mediator.Send(registroCommand);
                }
                catch (ValidationResultException ex)
                {
                    await _userManager.DeleteAsync(user);
                    return Falha(ex);
                }
                catch (Exception)
                {
                    await _userManager.DeleteAsync(user);
                    return Falha("Não foi possível cadastrar o evento");
                }

                _logger.LogInformation(1, "Usuario criado com sucesso!");

                var token = GerarTokenUsuario(new LoginViewModel { Email = model.Email, Senha = model.Senha });
                return Sucesso(token);
            }

            return Falha(result);
        }

        [HttpPost]
        [AllowAnonymous]
        [VersionedRoute("api/conta")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Senha, false, true);

            if (result.Succeeded)
            {
                _logger.LogInformation(1, "Usuario logado com sucesso");
                var token = GerarTokenUsuario(model);
                return Sucesso(token);
            }

            return Falha("Usuário e/ou senha inválidos");
        }

        private async Task<object> GerarTokenUsuario(LoginViewModel login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);
            var userClaims = await _userManager.GetClaimsAsync(user);

            userClaims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            // Necessário converver para IdentityClaims
            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(userClaims);

            var handler = new JwtSecurityTokenHandler();
            var signingConf = new SigningCredentialsConfiguration();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenDescriptor.Issuer,
                Audience = _tokenDescriptor.Audience,
                SigningCredentials = signingConf.SigningCredentials,
                Subject = identityClaims,
                NotBefore = DateTime.Now,
                Expires = DateTime.Now.AddMinutes(_tokenDescriptor.MinutesValid)
            });

            var encodedJwt = handler.WriteToken(securityToken);
            var orgUser = _organizadorRepository.ObterPorId(Guid.Parse(user.Id));

            var response = new
            {
                access_token = encodedJwt,
                expires_in = DateTime.Now.AddMinutes(_tokenDescriptor.MinutesValid),
                user = new
                {
                    id = user.Id,
                    nome = orgUser.Nome,
                    email = orgUser.Email,
                    claims = userClaims.Select(c => new { c.Type, c.Value })
                }
            };

            return response;
        }
    }
}