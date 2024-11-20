using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfBiomEtec.Models
{
    public sealed class CadastroUsuario
    {
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public string Permissao { get; set; }

        public CadastroUsuario(string usuario, string senha, string permissao)
        {
            Usuario = usuario;
            Senha = senha;
            Permissao = permissao;
        }

        public CadastroUsuario() { }
    }
}
