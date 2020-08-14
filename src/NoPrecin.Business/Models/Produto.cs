using System;
using System.Collections.Generic;
using System.Text;

namespace NoPrecin.Business.Models
{
	public class Produto : Entity
	{
		public string Nome { get; set; }
		public string Descricao { get; set; }
		public string Imagem { get; set; }
		public decimal Valor { get; set; }
		public DateTime DataCadastro { get; set; }
		public bool Ativo { get; set; }
		public string EmailProprietario { get; set; }

		


		public TipoProduto TipoProduto { get; set; }

		/* EF */
		public Venda Venda { get; set; }

	}
}
