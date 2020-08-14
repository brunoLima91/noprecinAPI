using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using NoPrecin.API.Extentions;
using NoPrecin.Business.Interfaces;
using NoPrecin.Business.Models;
using NoPrecin.Business.Notificacoes;
using NoPrecin.Business.Services;
using NoPrecin.Data.Context;
using NoPrecin.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoPrecin.API.Configuration
{
	public static class DependencyInjectionConfig
	{
		public static IServiceCollection ResolveDependecies(this IServiceCollection services)
		{
			services.AddScoped<MeuDBContext>();
			services.AddScoped<IProdutoRepository, ProdutoRepository>();
			services.AddScoped<INotificador, Notificador>();			
			services.AddScoped<IProdutoService, ProdutoService>();

			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			services.AddScoped<IUser, AspNetUser>();

			services.AddScoped<IVendaRepository, VendaRepository>();
			services.AddScoped<IVendaService, VendaService>();

			return services;
		}
	}
}
