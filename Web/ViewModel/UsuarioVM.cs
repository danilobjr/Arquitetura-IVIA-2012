using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModel
{
    public class UsuarioVM
    {
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        public string ConfirmarSenha { get; set; }
        public string Mensagem { get; set; }
    }
}