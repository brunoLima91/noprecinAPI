using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NoPrecin.API.Extentions;
using NoPrecin.API.ViewModels;
using NoPrecin.Business.Interfaces;
using NoPrecin.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoPrecin.API.Controllers
{
	[Authorize]
	[Route("api/produtos")]
	public class ProdutosController : MainController
	{
		private readonly IProdutoRepository _produtoRepository;
		private readonly IProdutoService _produtoService;
		private readonly IMapper _mapper;
		private readonly UserManager<IdentityUser> _userManager;

		public ProdutosController(IProdutoService produtoService,
			UserManager<IdentityUser> userManager,
			IProdutoRepository produtoRepository,
			IMapper mapper,
			INotificador notificador,
			IUser user
			) : base(notificador, user)
		{
			_produtoRepository = produtoRepository;
			_produtoService = produtoService;
			_mapper = mapper;
			_userManager = userManager;
		}

		[AllowAnonymous]
		[HttpGet]
		public async Task<IEnumerable<ProdutoViewModelResponse>> ObterTodos()
		{
			return _mapper.Map<IEnumerable<ProdutoViewModelResponse>>(await _produtoRepository.ObterTodos());
		}

		//[ClaimAuthorize("Produto","ler")]
		[HttpGet("{id:guid}")]
		public async Task<ProdutoViewModelResponse> ObterPorId(Guid id)
		{
			return await ObterProduto(id);
		}

		//[ClaimAuthorize("Produto", "ler")]
		[HttpGet("por-usuario/{id:guid}")]
		public async Task<IEnumerable<ProdutoViewModelResponse>> ObterPorUsuario(Guid id)
		{
			var lusuario = await _userManager.FindByIdAsync(id.ToString());
			if (lusuario == null)
				return null;


			return _mapper.Map<IEnumerable<ProdutoViewModelResponse>>(await _produtoRepository.Buscar(x => x.EmailProprietario == lusuario.Email));




		}

		[HttpDelete("{id:guid}")]
		public async Task<ActionResult<ProdutoViewModelResponse>> Excluir(Guid id)
		{
			var produto = await ObterProduto(id);

			if (produto == null) return NotFound();

			await _produtoService.Remover(id);

			return CustomResponse(produto);
		}

		[HttpPost]
		public async Task<ActionResult<ProdutoViewModelResponse>> Adcionar(ProdutoViewModelRequest produtoViewModel)
		{
			if (!ModelState.IsValid) return CustomResponse(ModelState);			

			return CustomResponse(_mapper.Map<ProdutoViewModelResponse>(await _produtoService.Adicionar(_mapper.Map<Produto>(produtoViewModel))));
		}

		private async Task<ProdutoViewModelResponse> ObterProduto(Guid id)
		{
			return _mapper.Map<ProdutoViewModelResponse>(await _produtoRepository.ObterProdutoPorId(id));

		}

		[HttpPut("{id:guid}")]
		public async Task<IActionResult> Atualizar(Guid id, ProdutoViewModelRequest produtoViewModel)
		{
			if (id != produtoViewModel.Id)
			{
				NotificarErro("Os ids informados não são iguais");
				return CustomResponse();
			}

			if (!ModelState.IsValid) return CustomResponse(ModelState);
			var produtoAtualizacao = await ObterProduto(id);




			produtoAtualizacao.Nome = produtoViewModel.Nome;
			produtoAtualizacao.Descricao = produtoViewModel.Descricao;
			produtoAtualizacao.Valor = produtoViewModel.Valor;
			produtoAtualizacao.Ativo = produtoViewModel.Ativo;
			produtoAtualizacao.Imagem = produtoViewModel.Imagem;

			await _produtoService.Atualizar(_mapper.Map<Produto>(produtoAtualizacao));

			return CustomResponse(produtoAtualizacao);
		}
	}
}
