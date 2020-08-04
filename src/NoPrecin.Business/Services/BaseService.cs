using NoPrecin.Business.Interfaces;
using NoPrecin.Business.Notificacoes;
using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation.Results;

namespace NoPrecin.Business.Services
{
	public abstract class BaseService
	{
		private readonly INotificador _notificador;
		public BaseService(INotificador notificador)
		{
			_notificador = notificador;
		}
		protected void Notificar(ValidationResult validationResult)
		{
			foreach (var error in validationResult.Errors)
			{
				Notificar(error.ErrorMessage);
			}
		}

		protected void Notificar(string mensagem)
		{
			_notificador.Handle(new Notificacao(mensagem));

		}

	
	}
}
