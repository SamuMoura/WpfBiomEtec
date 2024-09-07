using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfBiomEtec
{
    internal sealed class CadastroResp : Cadastro
    {
        private string relacionamentocAluno;

        public string RelacionamentocAluno { get => relacionamentocAluno; set => relacionamentocAluno = value; }

        public CadastroResp(string idBiometria, string nome, string cpf, string email, string telefone)
        {
            this.IdBiometria = idBiometria;
            this.Nome = nome;
            this.CPF = cpf;
            this.Email = email;
            this.Telefone = telefone;

            this.relacionamentocAluno = RelacionamentocAluno;
        }

        public CadastroResp() { }

    }
}

