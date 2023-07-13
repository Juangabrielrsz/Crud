using Crud_Vaga_Estagio.Models;
using Microsoft.EntityFrameworkCore;

namespace Crud_Vaga_Estagio.Data
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options)
            : base(options)
        { }

        public DbSet<Empresa> Empresas { get; set; }
    }
}
