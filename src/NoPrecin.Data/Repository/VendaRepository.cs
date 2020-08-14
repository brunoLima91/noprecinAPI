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
	public class VendaRepository : Repository<Venda>, IVendaRepository
	{
		public VendaRepository(MeuDBContext db) : base(db)
		{
		}

		public async Task<Venda> ObterVendaProduto(Guid IdVenda)
		{
			return await Db.Vendas.AsNoTracking().Include(p => p.Produto)
				.FirstAsync(v => v.Id == IdVenda);
		}

		public async Task<IEnumerable<Venda>> ObterVendasProduto()
		{
			return await Db.Vendas.AsNoTracking().Include(p => p.Produto).ToListAsync();
				
		}
	}
}
