using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjetoApi.Models;

namespace ProjetoApi.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Partido> Partidos { get; set; }
        public DbSet<Politico> Politicos { get; set; }
        public DbSet<Processo> Processos { get; set; }
        public DbSet<ProjetoLei> ProjetosLeis { get; set; }
        public DbSet<Representante> Representantes { get; set; }
        public DbSet<Telefone> Telefones { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

    }
}