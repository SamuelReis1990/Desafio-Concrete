using DesafioConcrete.Dominio.Entidades;
using System.Data.Entity.ModelConfiguration;

namespace DesafioConcrete.Infra.Mapeamentos
{
    public class UsuarioMap : EntityTypeConfiguration<Usuario>
    {
        public UsuarioMap()
        {
            ToTable("usuario");

            HasKey(t => t.Id);
            Property(t => t.Id).HasColumnName("id");            
            Property(t => t.Nome).HasColumnName("nome").HasMaxLength(50);
            Property(t => t.Email).HasColumnName("email").HasMaxLength(50).IsRequired();
            Property(t => t.Token).HasColumnName("token").HasMaxLength(256).IsRequired();
            Property(t => t.Senha).HasColumnName("senha").HasMaxLength(128).IsRequired();
            Property(t => t.DataCriacao).HasColumnName("data_criacao").IsOptional();
            Property(t => t.DataAtualizacao).HasColumnName("data_atualizacao").IsOptional();
            Property(t => t.UltimoLogin).HasColumnName("ultimo_login").IsOptional();            
        }
    }
}
