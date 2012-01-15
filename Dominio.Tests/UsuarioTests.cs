using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dominio.Tests.Fakes;
using Dominio.Entidades;
using Dominio;
using Dominio.Servicos;
using IVIA.Comum.Exceptions;

namespace Dominio.Tests
{
    [TestClass]
    public class Ao_Alterar_Um_Usuario
    {
        IDominioContext ctx;

        [TestInitialize]
        public void Setup()
        {
            ctx = new DominioContextFake
            {
                Usuarios = 
                {
                    new Usuario {Id = 1, Nome = "Junior", Email = "junior@ivia.com.br", Senha = "!@#"},
                    new Usuario {Id = 2, Nome="Bruno", Email="bruno@ivia.com.br", Senha="123"}
                }
            };
        }

        [TestMethod]
        public void Nome_Senha_Email_Sao_Obrigatorios()
        {

            var gerenteUsuarios = new GerenteUsuarios(ctx);
            try
            {
                gerenteUsuarios.CriarNovoUsuario("", "", "");
                Assert.Fail("Deveria ter falhado");
            }
            catch (RegrasDeNegocioException rne)
            {
                var msg1 = rne.Erros.Where(x => x.Mensagem.Contains("Nome do usuário")).FirstOrDefault();
                var msg2 = rne.Erros.Where(x => x.Mensagem.Contains("Senha do usuário")).FirstOrDefault();
                var msg3 = rne.Erros.Where(x => x.Mensagem.Contains("Email do usuário")).FirstOrDefault();

                Assert.IsNotNull(msg1);
                Assert.IsNotNull(msg2);
                Assert.IsNotNull(msg3);
            }
        }

        [TestMethod]
        public void Seu_Email_Deve_Ser_Unico()
        {
            var emailJaExistente = "junior@ivia.com.br";
            var gerenteUsuarios = new GerenteUsuarios(ctx);

            try
            {
                var usuariaParaEditar = ctx.Usuarios.Single(u => u.Id == 2);
                usuariaParaEditar.Email = emailJaExistente;

                gerenteUsuarios.ValidarAlterarUsuario(usuariaParaEditar);


                Assert.Fail("Deveria ter falhado");
            }
            catch (RegrasDeNegocioException rne)
            {
                var msg = rne.Erros.Where(x => x.Mensagem.Contains("já existe")).FirstOrDefault();
                Assert.IsNotNull(msg);
            }
        }


    }

    [TestClass]
    public class Ao_Criar_Um_Usuario
    {
        IDominioContext ctx;

        [TestInitialize]
        public void Setup()
        {
            ctx = new DominioContextFake
            {
                Usuarios = 
                {
                    new Usuario {Id = 1, Nome = "Junior", Email = "junior@ivia.com.br", Senha = "!@#"}
                }
            };
        }

        [TestMethod]
        public void Nome_Senha_Email_Sao_Obrigatorios()
        {
           
            var gerenteUsuarios = new GerenteUsuarios(ctx);
            try
            {
                gerenteUsuarios.CriarNovoUsuario("","","");
                Assert.Fail("Deveria ter falhado");
            }
            catch (RegrasDeNegocioException rne)
            {
                var msg1 = rne.Erros.Where(x => x.Mensagem.Contains("Nome do usuário")).FirstOrDefault();
                var msg2 = rne.Erros.Where(x => x.Mensagem.Contains("Senha do usuário")).FirstOrDefault();
                var msg3 = rne.Erros.Where(x => x.Mensagem.Contains("Email do usuário")).FirstOrDefault();

                Assert.IsNotNull(msg1);
                Assert.IsNotNull(msg2);
                Assert.IsNotNull(msg3);
            }
        }

        [TestMethod]
        public void Seu_Email_Deve_Ser_Unico()
        {
            var emailJaExistente = "junior@ivia.com.br";
            var gerenteUsuarios = new GerenteUsuarios(ctx);

            try
            {
                gerenteUsuarios.CriarNovoUsuario("aa","123",emailJaExistente);
                Assert.Fail("Deveria ter falhado");
            }
            catch (RegrasDeNegocioException rne)
            {
                var msg = rne.Erros.Where(x => x.Mensagem.Contains("já existe")).FirstOrDefault();
                Assert.IsNotNull(msg);
            }
        }

        [TestMethod]
        public void Seu_Email_Deve_Estar_Em_Formato_Valido()
        {
            var emailinvalido = "junior.com.br";
            var gerenteUsuarios = new GerenteUsuarios(ctx);

            try
            {
                gerenteUsuarios.CriarNovoUsuario("aa","123",emailinvalido);
                Assert.Fail("Deveria ter falhado");
            }
            catch (RegrasDeNegocioException rne)
            {
                var msg = rne.Erros.Where(x => x.Mensagem.Contains("Email inválido")).FirstOrDefault();
                Assert.IsNotNull(msg);
            }
        }

    }
}
