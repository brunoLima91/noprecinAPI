using NoPrecin.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NoPrecin.Business.Interfaces
{
	public interface IVendaRepository :IRepository<Venda>
	{
		Task<Venda> ObterVendaProduto(Guid IdVenda);
		Task<IEnumerable<Venda>> ObterVendasProduto();
	}
}
