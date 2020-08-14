using System;
using System.Collections.Generic;
using System.Text;

namespace NoPrecin.Business.Models
{
	public class Venda: Entity
	{		
		public Guid ProdutoId { get; set; }
		public DateTime Data { get; set; }
		public string EmailComprador { get; set; }

		/* EF */
		public Produto Produto { get; set; }
	}
}
