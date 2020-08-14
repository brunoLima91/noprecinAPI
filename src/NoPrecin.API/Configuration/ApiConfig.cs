using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoPrecin.API.Configuration
{
	public static class ApiConfig
	{
		public static IServiceCollection WebApiConfig(this IServiceCollection services)
		{
			services.AddControllers();			

			services.Configure<ApiBehaviorOptions>(options => {
				options.SuppressModelStateInvalidFilter = true;
			});

			services.AddCors(options =>
			{
				options.AddPolicy("Development",
					builder => builder
					.AllowAnyOrigin()					
					.AllowAnyMethod()
					.AllowAnyHeader()					
					);
			});

			//services.AddSwaggerGen(c =>
			//{
			//	c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
			//	{
			//		Version = "v1",
			//		Title = "NoPrecin API",
			//		Description = "API desenvolvida para o projeto NoPrecin",
			//		Contact = new Microsoft.OpenApi.Models.OpenApiContact
			//		{
			//			Name = "NoPrecin Supports",
			//			Email = "noprecin@supports.com",
			//			Url = new Uri("https://github.com/brunoLima91/noprecinAPI")
			//		},
			//		License = new Microsoft.OpenApi.Models.OpenApiLicense
			//		{
			//			Name = "MIT",
			//			Url = new Uri("https://github.com/brunoLima91/noprecinAPI")
			//		}
			//	});
			//});

			services.AddSwaggerConfig();
			return services;
		}

		public static IApplicationBuilder UseMvcConfiguration(this IApplicationBuilder app)
		{

			app.UseHttpsRedirection();

			app.UseCors();
			app.UseAuthentication();
			app.UseRouting();
			app.UseAuthorization();

			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "NoPrecin API");
			});

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

			return app;
		}
	}
}
