using NoPrecin.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NoPrecin.Business.Interfaces
{
	public interface IVendaService : IDisposable
	{
		Task<Venda> Adicionar(Venda venda);
		Task Atualizar(Venda venda);
		Task Remover(Guid id);
	}
}
