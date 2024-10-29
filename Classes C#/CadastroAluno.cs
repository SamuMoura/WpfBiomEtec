using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfBiomEtec
{
    internal sealed class CadastroAluno : Cadastro
    {

        public CadastroAluno(string idBiometria, string nome, string cpf, string email, string telefone, int rm)
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

