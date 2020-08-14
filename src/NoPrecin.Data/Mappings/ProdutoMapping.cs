using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NoPrecin.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoPrecin.Data.Mappings
{
	public class ProdutoMapping : IEntityTypeConfiguration<Produto>
	{
		public void Configure(EntityTypeBuilder<Produto> builder)
		{
			builder.HasKey(p => p.Id);

			builder.Property(p => p.Nome)
				.IsRequired()
				.HasColumnType("varchar(200)");

			builder.Property(p => p.Descricao)
				.IsRequired()
				.HasColumnType("varchar(1000)");

			builder.Property(p => p.Imagem)
				.IsRequired()
				.HasColumnType("varchar(100)");

			builder.Property(p => p.EmailProprietario)
				.IsRequired()
				.HasColumnType("varchar(100");

			// 1: 1 => Produto: Venda
			builder.HasOne(f => f.Venda)
				.WithOne(e => e.Produto)
				.OnDelete(DeleteBehavior.Cascade);

			builder.ToTable("Produtos");
		}


	}
}
