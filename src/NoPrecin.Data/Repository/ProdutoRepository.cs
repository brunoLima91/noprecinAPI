using Microsoft.EntityFrameworkCore;
using NoPrecin.Business.Interfaces;
using NoPrecin.Business.Models;
using NoPrecin.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoPrecin.Data.Repository
{
	public class ProdutoRepository : Repository<Produto>, IProdutoRepository
	{
		public ProdutoRepository(MeuDBContext context) : base(context)
		{

		}

		public async Task<Produto> ObterProdutoPorId(Guid id)
		{
			return await Db
				.Produtos
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task AtualizarProduto(Produto produto)
		{
			
			
			await base.Atualizar(produto);
		}


	}
}
