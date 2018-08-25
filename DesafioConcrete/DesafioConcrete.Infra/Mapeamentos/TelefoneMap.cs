using DesafioConcrete.Dominio.Entidades;
using System.Data.Entity.ModelConfiguration;

namespace DesafioConcrete.Infra.Mapeamentos
{
    public class TelefoneMap : EntityTypeConfiguration<Telefone>
    {
        public TelefoneMap()
        {
            ToTable("telefone");

            HasKey(t => t.Id);
            Property(t => t.Id).HasColumnName("id");
            Property(t => t.UsuarioId).HasColumnName("id_usuario");
            Property(t => t.DDD).HasColumnName("ddd").IsOptional();
            Property(t => t.Numero).HasColumnName("numero").IsOptional();

            HasRequired(t => t.Usuario).WithMany(m => m.Telefone).HasForeignKey(f => f.UsuarioId);
        }
    }
}
