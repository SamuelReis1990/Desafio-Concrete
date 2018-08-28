using DesafioConcrete.Dominio.Entidades;
using DesafioConcrete.Infra.Mapeamentos;
using System.Data.Entity;

namespace DesafioConcrete.Infra.Contextos
{
    public class EFContexto : DbContext
    {
        public EFContexto() : base("ContextoConnectionString")
        {
            Database.SetInitializer(new ContextoInicializador());
        }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Telefone> Telefones { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Configuration.ProxyCreationEnabled = false;

            modelBuilder.Configurations.Add(new UsuarioMap());
            modelBuilder.Configurations.Add(new TelefoneMap());

            base.OnModelCreating(modelBuilder);            
        }
    }

    public class ContextoInicializador : DropCreateDatabaseIfModelChanges<EFContexto>
    {

    }
}
