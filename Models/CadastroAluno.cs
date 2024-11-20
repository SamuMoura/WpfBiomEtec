using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;


namespace WpfBiomEtec.Models
{
    public sealed class CadastroAluno : Cadastro
    {
        private int rm;

        public int RM { get => rm; set => rm = value; }

        public CadastroAluno(string idBiometria, string nome, string cpf, string email, string telefone)
        {
            this.IdBiometria = idBiometria;
            this.Nome = nome;
            this.CPF = cpf;
            this.Email = email;
            this.Telefone = telefone;

            this.RM = rm;
        }

        public CadastroAluno() { }

    }
}

