using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfBiomEtec
{
    internal sealed class CadastroUsuario
    {
        private string usuario;
        private string senha;
        private string permissao;
        private string biometria;

        public string Usuario { get => usuario; set => usuario = value; }
        public string Senha { get => senha; set => senha = value; }
        public string Permissao { get => permissao; set => permissao = value; }
        public string Biometria { get => biometria; set => biometria = value; }

        public CadastroUsuario(string usuario, string senha, string permissao, string biometria)
        {
            this.Usuario = usuario;
            this.Senha = senha;
            this.Permissao = permissao;
            this.Biometria = biometria;
        }

        public CadastroUsuario() { }
    }
}
