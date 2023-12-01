﻿using Jcf.Estacionamento.Api.Extensoes;
using Jcf.Estacionamento.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Jcf.Estacionamento.Api.Data.Contextos
{
    public class AppContexto : DbContext
    {
        public AppContexto(DbContextOptions<AppContexto> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
        }
    }
}
