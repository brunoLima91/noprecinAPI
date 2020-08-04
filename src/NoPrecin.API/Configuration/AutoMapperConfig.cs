using AutoMapper;
using NoPrecin.API.ViewModels;
using NoPrecin.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoPrecin.API.Configuration
{
	public class AutoMapperConfig : Profile
	{
		public AutoMapperConfig()
		{
			
			CreateMap<ProdutoViewModel, Produto>().ReverseMap();

			
		}
	}
}
