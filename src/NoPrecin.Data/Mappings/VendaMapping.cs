using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NoPrecin.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoPrecin.Data.Mappings
{
	public class VendaMapping : IEntityTypeConfiguration<Venda>
	{
		public void Configure(EntityTypeBuilder<Venda> builder)
		{
			builder.HasKey(p => p.Id);

			builder.Property(p => p.EmailComprador)
			.IsRequired()
			.HasColumnType("varchar(200)");

			builder.ToTable("Vendas");

		}
	}
}
