using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IVIA.Comum.Exceptions;

namespace Web
{
    public static class RegrasDeNegocioExceptionExtensions
    {
        public static void CopiarPara(this RegrasDeNegocioException ex, ModelStateDictionary modelState)
        {
            CopiarPara(ex, modelState, null);
        }

        public static void CopiarPara(this RegrasDeNegocioException ex, ModelStateDictionary modelState, string prefixo)
        {
            prefixo = string.IsNullOrEmpty(prefixo) ? "" : prefixo + ".";

            foreach (var prop in ex.Erros)
            {
                string chave = ExpressionHelper.GetExpressionText(prop.Propriedade);
                modelState.AddModelError(prefixo + chave, prop.Mensagem);
            }
        }
    }
}