using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoPrecin.API.Configuration
{
	public static class SwaggerConfig
	{
		public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
		{
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
				{
					Version = "v1",
					Title = "NoPrecin API",
					Description = "API desenvolvida para o projeto NoPrecin",
					Contact = new Microsoft.OpenApi.Models.OpenApiContact
					{
						Name = "NoPrecin Supports",
						Email = "noprecin@supports.com",
						Url = new Uri("https://github.com/brunoLima91/noprecinAPI")
					},
					License = new Microsoft.OpenApi.Models.OpenApiLicense
					{
						Name = "MIT",
						Url = new Uri("https://github.com/brunoLima91/noprecinAPI")
					}
				});


				c.OperationFilter<SwaggerDefaultValues>();
				var security = new OpenApiSecurityRequirement();
				security.Add(
				new OpenApiSecurityScheme() { BearerFormat = "Bearer" }, new string[] { }


				);

				c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Description = "Insira o token JWT desta maneira: Bearer {seu token}",
					Name = "Authorization",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.ApiKey
				});

				c.AddSecurityRequirement(security);

			});



			return services;
		}
	}

	public class SwaggerDefaultValues : Swashbuckle.AspNetCore.SwaggerGen.IOperationFilter
	{

		public void Apply(OpenApiOperation operation, OperationFilterContext context)
		{
			var apiDescription = context.ApiDescription;

			//operation.Deprecated = apiDescription.IsDeprecated();

			if (operation.Parameters == null)
			{
				return;
			}

			foreach (var parameter in operation.Parameters.OfType<OpenApiParameter>())
			{
				var description = apiDescription.ParameterDescriptions.First(p => p.Name == parameter.Name);

				if (parameter.Description == null)
				{
					parameter.Description = description.ModelMetadata?.Description;
				}

				//if (parameter.default == null)
				//{
				//	parameter.default = description.DefaultValue;
				//}

				parameter.Required |= description.IsRequired;
			}
		}
	}
}
