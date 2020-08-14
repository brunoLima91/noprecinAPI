using NoPrecin.Business.Interfaces;
using NoPrecin.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoPrecin.Business.Services
{
	public class ProdutoService : BaseService, IProdutoService
	{
		private readonly IProdutoRepository _produtoRepository;
		private readonly IUser _user;
		private readonly IVendaRepository _vendaRepository;
		public ProdutoService(IProdutoRepository produtoRepository,
			IVendaRepository vendaRepository,
			IUser user,
			INotificador notificador) : base(notificador)
		{
			_produtoRepository = produtoRepository;
			_user = user;
			_vendaRepository = vendaRepository;
		}
		public async Task<Produto> Adicionar(Produto produto)
		{
			
			// Adicionar validação
			if (produto == null)
				return null;

			produto.EmailProprietario = _user?.GetUserEmail();
			produto.Ativo = true;
			produto.DataCadastro = DateTime.Now;
			produto.Imagem = produto.Imagem ?? "Sem Imagem";

			await _produtoRepository.Adicionar(produto);
			return produto;
		}

		public async Task Atualizar(Produto produto)
		{

			if ((await _vendaRepository.Buscar(v=> v.ProdutoId == produto.Id)).Count() > 0)
			{
				Notificar("Produto já vendido não pode ser atualizado.");
				return;
			}
			
			// Adicionar validação
			if (String.IsNullOrEmpty(produto.EmailProprietario))
			{
				produto.EmailProprietario = _user?.GetUserEmail();
			}

			if (String.IsNullOrEmpty(produto.Imagem))
			{
				produto.Imagem = "Sem Imagem";
			}
			

			await _produtoRepository.AtualizarProduto(produto);
		}

		public async Task Remover(Guid id)
		{
			if ((await _vendaRepository.Buscar(v => v.ProdutoId == id)).Count() > 0)
			{
				Notificar("Produto já vendido não pode ser excluído.");
				return;
			}
			await _produtoRepository.Remover(id);
		}

		public void Dispose()
		{
			_produtoRepository?.Dispose();
		}
	}
}
