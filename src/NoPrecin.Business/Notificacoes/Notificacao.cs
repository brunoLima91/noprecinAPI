using System;
using System.Collections.Generic;
using System.Text;

namespace NoPrecin.Business.Notificacoes
{
	public class Notificacao
	{
		public string Mensagem { get; }

		public Notificacao(string mensagem)
		{
			Mensagem = mensagem;
		}
	}
}
