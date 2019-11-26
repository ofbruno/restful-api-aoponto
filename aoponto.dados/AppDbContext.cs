using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aoponto.modelos;

namespace aoponto.dados
{
    public partial class AppDbContext : DbContext
    {
        public DbSet<Aparelho> Aparelhos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Raca> Racas { get; set; }
        public DbSet<Cidade> Cidades { get; set; }
        public DbSet<Produtor> Produtores { get; set; }
        public DbSet<Tecnico> Tecnicos { get; set; }
        public DbSet<Frigorifico> Frigorificos { get; set; }
        public DbSet<Lote> Lotes { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<LoteEtapa> LoteEtapas { get; set; }
        public DbSet<LoteRegistro> LoteRegistros { get; set; }
        public DbSet<LoteAnexo> LoteAnexos { get; set; }
        public DbSet<Notificacao> Notificacoes { get; set; }
        public DbSet<NotificacaoUsuario> NotificacoesUsuarios { get; set; }
        public DbSet<Push> Pushes { get; set; }
        public DbSet<Noticia> Noticias { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aparelho>().HasOne(a => a.Usuario);

            modelBuilder.Entity<Produtor>().HasOne(p => p.Usuario);
            modelBuilder.Entity<Frigorifico>().HasOne(p => p.Usuario);
            modelBuilder.Entity<Tecnico>().HasOne(p => p.Usuario);

            modelBuilder.Entity<Lote>().HasOne(o => o.Vendedor).WithMany().HasForeignKey(o => o.UsuarioIdVendedor);
            modelBuilder.Entity<Lote>().HasOne(o => o.Comprador).WithMany().HasForeignKey(o => o.UsuarioIdComprador);
            modelBuilder.Entity<Lote>().HasOne(o => o.Raca);
            modelBuilder.Entity<Lote>().HasOne(o => o.Endereco);
            modelBuilder.Entity<Lote>().HasMany(o => o.Etapas).WithOne(l => l.Lote);
            modelBuilder.Entity<Lote>().HasMany(o => o.Registros).WithOne(l => l.Lote);
            modelBuilder.Entity<Lote>().HasMany(o => o.Anexos).WithOne(l => l.Lote);

            modelBuilder.Entity<LoteEtapa>().HasOne(e => e.Usuario);
            modelBuilder.Entity<LoteEtapa>().HasOne(e => e.Lote);

            modelBuilder.Entity<LoteRegistro>().HasOne(r => r.Lote);

            modelBuilder.Entity<LoteAnexo>().HasOne(r => r.Lote);

            modelBuilder.Entity<Cidade>().HasOne(c => c.Estado);

            modelBuilder.Entity<Endereco>().HasOne(e => e.Cidade);
            modelBuilder.Entity<Endereco>().HasOne(e => e.Estado);
            modelBuilder.Entity<Endereco>().HasOne(e => e.Usuario);

            modelBuilder.Entity<Notificacao>().HasMany(n => n.Pushes).WithOne(p => p.Notificacao);
            modelBuilder.Entity<Notificacao>().HasMany(n => n.NotificacoesUsuarios).WithOne(nu => nu.Notificacao);
        }
    }
}
