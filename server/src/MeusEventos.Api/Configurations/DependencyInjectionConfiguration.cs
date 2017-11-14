using MediatR;
using MeusEventos.Domain.Core.Common;
using MeusEventos.Domain.Eventos.Repositories;
using MeusEventos.Domain.Organizadores.Repositories;
using MeusEventos.Infra.Data.Contexts;
using MeusEventos.Infra.Data.Repositories;
using MeusEventos.Infra.Identity.Models;
using MeusEventos.Infra.Identity.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using FeaturesEvento = MeusEventos.Domain.Eventos.Features;
using FeaturesOrganizador = MeusEventos.Domain.Organizadores.Features;

namespace MeusEventos.Api.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDIConfiguration(this IServiceCollection services)
        {
            // ASPNET
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //Data
            services.AddScoped<MeusEventosContext>();
            services.AddScoped<IEventoRepository, EventoRepository>();
            services.AddScoped<IOrganizadorRepository, OrganizadorRepository>();

            //Evento
            services.AddScoped<IAsyncRequestHandler<FeaturesEvento.Registro.Command, Guid>, FeaturesEvento.Registro.Handler>();

            //Organizador
            services.AddScoped<IAsyncRequestHandler<FeaturesOrganizador.Registro.Command, Guid>, FeaturesOrganizador.Registro.Handler>();

            // Infra - Identity
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
            services.AddScoped<IUser, AspNetUser>();
        }
    }
}
