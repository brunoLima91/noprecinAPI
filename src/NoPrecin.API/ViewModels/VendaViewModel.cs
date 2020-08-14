using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NoPrecin.API.ViewModels
{
	public class VendaViewModelRequest
	{
		public Guid ProdutoId { get; set; }

	}

	public class VendaViewModelResponse
	{ 
		[Key]
		public Guid Id { get; set; }
		public DateTime Data { get; set; }
		public string EmailComprador { get; set; }
		public Guid ProdutoId { get; set; }
		public ProdutoViewModelResponse Produto { get; set; }

	}
}
