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
			
			CreateMap<ProdutoViewModelRequest, Produto>().ReverseMap();
			CreateMap<ProdutoViewModelResponse, Produto>().ReverseMap();
			CreateMap<Venda, VendaViewModelResponse>().ReverseMap();
			CreateMap<Venda, VendaViewModelRequest>().ReverseMap();

		}
	}
}
