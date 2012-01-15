using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Dominio.Entidades;

namespace Dominio
{
    public interface IDominioContext 
    {
        IDbSet<Usuario> Usuarios { get; }
        int SaveChanges();
    }
}
