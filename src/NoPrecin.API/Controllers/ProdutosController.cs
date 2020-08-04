using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NoPrecin.API.ViewModels;
using NoPrecin.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoPrecin.API.Controllers
{
	
	[Route("api/produtos")]
	public class ProdutosController : MainController
	{
		private readonly IProdutoRepository _produtoRepository;
		private readonly IProdutoService _produtoService;
		private readonly IMapper _mapper;

		public ProdutosController(IProdutoService produtoService,
			IProdutoRepository produtoRepository,
			IMapper mapper,
			INotificador notificador
			) : base(notificador)
		{
			_produtoRepository = produtoRepository;
			_produtoService = produtoService;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<IEnumerable<ProdutoViewModel>> ObterTodos()
		{
			return _mapper.Map<IEnumerable<ProdutoViewModel>>(await _produtoRepository.ObterTodos());
		}
	}
}
