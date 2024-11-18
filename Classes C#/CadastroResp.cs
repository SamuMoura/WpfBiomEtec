using System;

namespace WpfBiomEtec
{
    public sealed class CadastroResp : Cadastro
    {
        public string Biometria { get; set; } // Renomeado de IdBiometria para Biometria para manter consistência
        public string RelacionamentoAluno { get; set; } // Renomeado para consistência com o restante do código
        public int? RM { get; set; } // Propriedade adicionada para suportar RM como valor opcional

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
