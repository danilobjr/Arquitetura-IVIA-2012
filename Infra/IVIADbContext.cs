using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Dominio.Entidades;
using Infra.Mapeamentos;
using Dominio;

namespace Infra
{
    public class IVIADbContext : DbContext, IDominioContext
    {
        public IVIADbContext()
            : base("IVIAConnectionString")
        {

        }

        public IDbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UsuarioConfiguration());
        }




    }
}
