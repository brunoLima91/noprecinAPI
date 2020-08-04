using NoPrecin.Business.Interfaces;
using NoPrecin.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NoPrecin.Business.Services
{
	public class ProdutoService : BaseService, IProdutoService
	{
		private readonly IProdutoRepository _produtoRepository;
		public ProdutoService(IProdutoRepository produtoRepository,
			INotificador notificador) : base(notificador)
		{
			_produtoRepository = produtoRepository;
		}
		public async Task Adicionar(Produto produto)
		{
			
			// Adicionar validação
			if (produto == null)
				return;
			
			
			produto.Ativo = true;
			produto.DataCadastro = DateTime.Now;

			await _produtoRepository.Adicionar(produto);
		}

		public async Task Atualizar(Produto produto)
		{
			// Adicionar validação
			await _produtoRepository.Atualizar(produto);
		}

		public async Task Remover(Guid id)
		{
			await _produtoRepository.Remover(id);
		}

		public void Dispose()
		{
			_produtoRepository?.Dispose();
		}
	}
}
