using System;

namespace WpfBiomEtec.Models
{
    public sealed class CadastroResp : Cadastro
    {
        public string Biometria { get; set; }
        public string RelacionamentoAluno { get; set; }
        public int? RM { get; set; }

        public CadastroResp(string biometria, string nome, string cpf, string email, string telefone, string relacionamentoAluno, int? rm = null)
        {
            Biometria = biometria;
            Nome = nome;
            CPF = cpf;
            Email = email;
            Telefone = telefone;
            RelacionamentoAluno = relacionamentoAluno;
            RM = rm;
        }

        public CadastroResp() { }
    }
}
