using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NoPrecin.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NoPrecin.Data.Context
{
	public class MeuDBContext: DbContext
	{
		public MeuDBContext(DbContextOptions<MeuDBContext> options): base(options)
		{

		}

		public DbSet<Produto> Produtos { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			foreach (var proprety in modelBuilder.Model.GetEntityTypes()
				.SelectMany(e => e.GetProperties()
				.Where(p => p.ClrType == typeof(string))))
				proprety.SetColumnType("varchar(100)");



			modelBuilder.ApplyConfigurationsFromAssembly(typeof(MeuDBContext).Assembly);

			foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;


			base.OnModelCreating(modelBuilder);
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.EnableSensitiveDataLogging();
		}
	}
}
