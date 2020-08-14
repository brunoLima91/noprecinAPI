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
	[Route("api/vendas")]
	public class VendasController : MainController
	{
		private readonly IVendaRepository _vendaRepository;
		private readonly IVendaService _vendaService;
		private readonly IMapper _mapper;
		private readonly UserManager<IdentityUser> _userManager;

		public VendasController(IVendaService vendaService,
			UserManager<IdentityUser> userManager,
			IVendaRepository vendaRepository,
			IMapper mapper,			
			INotificador notificador,
			IUser user) : base(notificador, user)
		{
			_vendaRepository = vendaRepository;
			_vendaService = vendaService;
			_mapper = mapper;
			_userManager = userManager;

		}

		[AllowAnonymous]
		[HttpGet]
		public async Task<IEnumerable<VendaViewModelResponse>> ObterTodos()
		{
			return _mapper.Map<IEnumerable<VendaViewModelResponse>>(await _vendaRepository.ObterVendasProduto());
		}

		//[ClaimAuthorize("Venda", "ler")]
		[HttpGet("{id:guid}")]
		public async Task<ActionResult<VendaViewModelResponse>> ObterPorId(Guid id)
		{
			var lVenda = await _vendaRepository.ObterPorId(id);
			if (lVenda == null)			
				return NotFound();
			

			return await ObterVenda(id);
		}

		//[ClaimAuthorize("Produto", "ler")]
		[HttpGet("por-usuario/{id:guid}")]
		public async Task<IEnumerable<VendaViewModelResponse>> ObterPorUsuario(Guid id)
		{
			var lusuario = await _userManager.FindByIdAsync(id.ToString());
			if (lusuario == null)
				return null;

			return _mapper.Map<IEnumerable<VendaViewModelResponse>>(await _vendaRepository.Buscar(x => x.EmailComprador == lusuario.Email));
		}

		[HttpDelete("{id:guid}")]
		public async Task<ActionResult<VendaViewModelResponse>> Excluir(Guid id)
		{
			var venda = await ObterVenda(id);

			if (venda == null) return NotFound();

			await _vendaService.Remover(id);

			return CustomResponse(venda);
		}

		[HttpPost]
		public async Task<ActionResult<VendaViewModelResponse>> Vender(VendaViewModelRequest vendaViewModelReQuest)
		{
			if (!ModelState.IsValid) return CustomResponse(ModelState);

			return CustomResponse(_mapper.Map<VendaViewModelResponse>(await _vendaService.Adicionar(_mapper.Map<Venda>(vendaViewModelReQuest))));
		}

		private async Task<VendaViewModelResponse> ObterVenda(Guid id)
		{
			return _mapper.Map<VendaViewModelResponse>(await _vendaRepository.ObterVendaProduto(id));

		}

		[HttpPut("{id:guid}")]
		public async Task<IActionResult> Atualizar(Guid id, VendaViewModelResponse vendaViewModel)
		{
			if (id != vendaViewModel.Id)
			{
				NotificarErro("Os ids informados não são iguais");
				return CustomResponse();
			}

			var vendaAtualizacao = await ObterVenda(id);
			if (!ModelState.IsValid) return CustomResponse(ModelState);



			vendaAtualizacao.Data = vendaViewModel.Data;
			vendaAtualizacao.EmailComprador = vendaViewModel.EmailComprador;
			await _vendaService.Atualizar(_mapper.Map<Venda>(vendaViewModel));

			return CustomResponse(vendaViewModel);
		}
	}
}
