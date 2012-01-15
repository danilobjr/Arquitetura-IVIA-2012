using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Dominio.Entidades;
using System.Data.Entity.ModelConfiguration;

namespace Infra.Mapeamentos
{
    class UsuarioConfiguration : EntityTypeConfiguration<Usuario>
    {
        public UsuarioConfiguration()
        {
            ToTable("T_USUARIO");
            HasKey(u => u.Id);
            Property(u => u.Id).HasColumnName("ID_USUARIO").IsRequired();
            Property(u => u.Email).HasColumnName("DS_EMAIL").HasMaxLength(100).IsRequired();
            Property(u => u.Nome).HasColumnName("NM_USUARIO").HasMaxLength(100).IsRequired();
            Property(u => u.Senha).HasColumnName("DS_SENHA").HasMaxLength(100).IsRequired();
        }
    }
}
