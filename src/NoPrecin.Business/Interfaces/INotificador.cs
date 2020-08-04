using NoPrecin.Business.Notificacoes;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoPrecin.Business.Interfaces
{
	public interface INotificador
	{
		bool TemNotificacao();
		List<Notificacao> ObterNotificacoes();
		void Handle(Notificacao notificacao);
	}
}
