using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Dominio.Entidades;
using IVIA.Comum.Fakes;

namespace Dominio.Tests.Fakes
{
    public class DominioContextFake : IDominioContext
    {
        public DominioContextFake()
        {
            this.Usuarios = new FakeDbSet<Usuario>();
            this.Perfis = new FakeDbSet<Perfil>();
        }

        public IDbSet<Usuario> Usuarios { get; private set; }
        public IDbSet<Perfil> Perfis { get; private set; }
        
        public int SaveChanges()
        {
            return 1;
        }
    }
}
