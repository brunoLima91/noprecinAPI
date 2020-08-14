using NoPrecin.Business.Interfaces;
using NoPrecin.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoPrecin.Business.Services
{
	public class VendaService : BaseService, IVendaService
	{
		private readonly IVendaRepository __vendaRepository;
		private readonly IUser _user;

		public VendaService(IVendaRepository vendaRepository,
			IUser user,
			INotificador notificador): base(notificador)
		{
			__vendaRepository = vendaRepository;
			_user = user;
		}
		
		public async Task<Venda> Adicionar(Venda venda)
		{
			if (venda == null)
				return null;

			if (venda.ProdutoId == Guid.Empty)
			{
				Notificar("Venda sem produto relacionado.");
			}
			else if((await __vendaRepository.Buscar(x => x.ProdutoId == venda.ProdutoId)).Count() > 0)
			{
				Notificar("Produto Já Foi Vendido.");
			}
			else
			{
				venda.EmailComprador = venda.EmailComprador ?? _user.GetUserEmail();
				venda.Data = DateTime.Now;

				await __vendaRepository.Adicionar(venda);
				
			}
			return await __vendaRepository.ObterVendaProduto(venda.Id);
		}

		public async Task Atualizar(Venda venda)
		{
			await __vendaRepository.Atualizar(venda);
		}

		public  void Dispose()
		{
			 __vendaRepository?.Dispose();
		}

		public async Task Remover(Guid id)
		{
			await __vendaRepository.Remover(id);
		}
	}
}
