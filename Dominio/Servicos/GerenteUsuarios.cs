using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.Entidades;
using IVIA.Comum.Exceptions;
using System.Text.RegularExpressions;

namespace Dominio.Servicos
{
	public class GerenteUsuarios
	{
		IDominioContext ctx;

		public GerenteUsuarios(IDominioContext ctx)
		{
			this.ctx = ctx;
		}

		public Usuario CriarNovoUsuario(string nome, string senha, string email)
		{
			var novoUsuario = new Usuario { Nome = nome, Senha = senha, Email = email };
			ValidarNovoUsuario(novoUsuario);
			return novoUsuario;
		}

        public void ValidarAlterarUsuario(Usuario usuario)
        {
            var violacaoDeRegras = new RegrasDeNegocioException<Usuario>();

            bool emailJaExiste = ctx.Usuarios.Any(u => u.Email == usuario.Email && u.Id != usuario.Id);

            if (emailJaExiste)
                violacaoDeRegras.AdicionarErro(u => u.Email, "E-mail já existe");

            ValidaCamposObrigatorios(usuario, violacaoDeRegras);

            if (violacaoDeRegras.Erros.Any())
            {
                throw violacaoDeRegras;
            }

        }

		private void ValidarNovoUsuario(Usuario novoUsuario)
		{
			var violacaoDeRegras = new RegrasDeNegocioException<Usuario>();

			ValidaCamposObrigatorios(novoUsuario, violacaoDeRegras);

			ValidaOutraRegras(novoUsuario, violacaoDeRegras);
		}

		private void ValidaOutraRegras(Usuario novoUsuario, RegrasDeNegocioException<Usuario> violacaoDeRegras)
		{
			if (EmailJaExiste(novoUsuario.Email))
			{
				violacaoDeRegras.AdicionarErro(x => x.Email, "Email já existe no cadastro");
			}

			if (!EmailValido(novoUsuario.Email))
			{
				violacaoDeRegras.AdicionarErro(x => x.Email, "Email inválido");
			}

			if (violacaoDeRegras.Erros.Any())
			{
				throw violacaoDeRegras;
			}
		}

		private static void ValidaCamposObrigatorios(Usuario novoUsuario, RegrasDeNegocioException<Usuario> violacaoDeRegras)
		{
			if (String.IsNullOrEmpty(novoUsuario.Nome))
			{
				violacaoDeRegras.AdicionarErro(x => x.Nome, "Nome do usuário é obrigatorio");
			}

			if (String.IsNullOrEmpty(novoUsuario.Senha))
			{
				violacaoDeRegras.AdicionarErro(x => x.Nome, "Senha do usuário é obrigatoria");
			}

			if (String.IsNullOrEmpty(novoUsuario.Email))
			{
				violacaoDeRegras.AdicionarErro(x => x.Nome, "Email do usuário é obrigatorio");
			}

			if (violacaoDeRegras.Erros.Any())
			{
				throw violacaoDeRegras;
			}
		}

		private bool EmailValido(string email)
		{
			 string MatchEmailPattern = 
				  @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
				+ @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
					[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
				+ @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
					[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
				+ @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";

			 if (email != null)
				 return Regex.IsMatch(email, MatchEmailPattern);
			 else
				 return false;
		}

		private bool EmailJaExiste(string email)
		{
			var usuario = ctx.Usuarios.Where(u => u.Email == email).FirstOrDefault();
			if (usuario != null)
				return true;
			return false;
		}

        
    }
}
