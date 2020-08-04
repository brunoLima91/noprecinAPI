using NoPrecin.Business.Interfaces;
using NoPrecin.Business.Models;
using NoPrecin.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoPrecin.Data.Repository
{
	public class ProdutoRepository : Repository<Produto>, IProdutoRepository
	{
		public ProdutoRepository(MeuDBContext context) : base(context)
		{

		}

	}
}
