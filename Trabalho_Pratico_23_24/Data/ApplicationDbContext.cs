using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Trabalho_Pratico_23_24.Models;

namespace Trabalho_Pratico_23_24.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Trabalho_Pratico_23_24.Models.Habitacao>? Habitacao { get; set; }
        public DbSet<Trabalho_Pratico_23_24.Models.Categorias>? Categorias { get; set; }
        public DbSet<Locador>? Locadores { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Gestor> Gestores { get; set; }
        public DbSet<Arrendamento> Arrendamentos { get; set; }
        public DbSet<CheckIn> CheckIns { get; set; }
        public DbSet<CheckOut> CheckOuts { get; set; }



    }
}