using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedeInova.Comum.Factories
{
    public interface IFabrica
    {
        T Obter<T>();
        T ObterRepositorio<T>(object contexto);
    }
}
