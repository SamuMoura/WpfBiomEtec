using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using WpfBiomEtec.Models;
using System.Windows;
using System;
using WpfBiomEtec.DAO;

namespace WpfBiomEtec.ViewModels
{
    public class WinCadRespViewModel : INotifyPropertyChanged
    {
        // Propriedades para os campos de entrada
        private string _relacionamentoAluno;
        private string _nome;
        private string _cpf;
        private string _email;
        private string _telefone;
        private string _idBiometria;
        private int? _rm;
        private CadastroResp _responsavelSelecionado;

        public string RelacionamentoAluno
        {
            get => _relacionamentoAluno;
            set
            {
                _relacionamentoAluno = value;
                OnPropertyChanged(nameof(RelacionamentoAluno));
            }
        }

        public string Nome
        {
            get => _nome;
            set
            {
                _nome = value;
                OnPropertyChanged(nameof(Nome));
            }
        }

        public string CPF
        {
            get => _cpf;
            set
            {
                _cpf = value;
                OnPropertyChanged(nameof(CPF));
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public string Telefone
        {
            get => _telefone;
            set
            {
                _telefone = value;
                OnPropertyChanged(nameof(Telefone));
            }
        }

        public string IdBiometria
        {
            get => _idBiometria;
            set
            {
                _idBiometria = value;
                OnPropertyChanged(nameof(IdBiometria));
            }
        }

        public int? RM
        {
            get => _rm;
            set
            {
                _rm = value;
                OnPropertyChanged(nameof(RM));
            }
        }

        public CadastroResp ResponsavelSelecionado
        {
            get => _responsavelSelecionado;
            set
            {
                _responsavelSelecionado = value;
                OnPropertyChanged(nameof(ResponsavelSelecionado));
                // Carregar os dados do responsável selecionado nos campos de entrada
                CarregarDadosResponsavel();
            }
        }

        // Lista de responsáveis
        public ObservableCollection<CadastroResp> Responsaveis { get; set; }

        // Comandos
        public ICommand CadastrarCommand { get; }
        public ICommand VoltarCommand { get; }
        public ICommand ExcluirCommand { get; }
        public ICommand CarregarCommand { get; }
        public ICommand SalvarCommand { get; }

        public WinCadRespViewModel()
        {
            // Inicializa os comandos
            CadastrarCommand = new RelayCommand(CadastrarResp, CanExecuteCadastrar);
            VoltarCommand = new RelayCommand(Voltar);
            ExcluirCommand = new RelayCommand(ExcluirResp);
            CarregarCommand = new RelayCommand(CarregarResponsaveis);
            SalvarCommand = new RelayCommand(SalvarResponsavel, CanExecuteSalvar);

            Responsaveis = new ObservableCollection<CadastroResp>();
            CarregarResponsaveis(null);
        }

        // Comando para voltar
        private void Voltar(object parameter)
        {
            var menuWindow = new WinMenu();
            menuWindow.Show();
            Application.Current.Windows[0]?.Close();
        }

        // Validação do cadastro
        private bool CanExecuteCadastrar(object parameter)
        {
            return !string.IsNullOrWhiteSpace(Nome) &&
                   !string.IsNullOrWhiteSpace(CPF) &&
                   !string.IsNullOrWhiteSpace(Email) &&
                   !string.IsNullOrWhiteSpace(IdBiometria) &&
                   !string.IsNullOrWhiteSpace(RelacionamentoAluno) &&
                   !string.IsNullOrWhiteSpace(Telefone) &&
                   RM.HasValue;
        }

        // Cadastrar novo responsável
        private void CadastrarResp(object parameter)
        {
            try
            {
                var resp = new CadastroResp(IdBiometria, Nome, CPF, Email, Telefone, RelacionamentoAluno, RM);
                InsertRespDAO.InserirResp(resp);
                Responsaveis.Add(resp);
                MessageBox.Show("Responsável cadastrado com sucesso!");
                LimparCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao cadastrar responsável: {ex.Message}");
            }
        }

        // Excluir responsável
        private void ExcluirResp(object parameter)
        {
            if (parameter is CadastroResp respSelecionado)
            {
                InsertRespDAO.ExcluirResp(respSelecionado);
                Responsaveis.Remove(respSelecionado);
                MessageBox.Show("Responsável excluído com sucesso!");
            }
        }

        // Carregar responsáveis do banco
        private void CarregarResponsaveis(object parameter)
        {
            var listaResp = InsertRespDAO.ListarResponsaveis();
            Responsaveis.Clear();
            foreach (var resp in listaResp)
            {
                Responsaveis.Add(resp);
            }
        }

        // Salvar (inserir ou atualizar) responsável
        private void SalvarResponsavel(object parameter)
        {
            try
            {
                if (ResponsavelSelecionado == null) return;

                // Se o responsável já existir, atualiza
                var respExistente = Responsaveis.FirstOrDefault(r => r.IdBiometria == ResponsavelSelecionado.IdBiometria);
                if (respExistente != null)
                {
                    // Atualiza os dados
                    respExistente.Nome = Nome;
                    respExistente.CPF = CPF;
                    respExistente.Email = Email;
                    respExistente.Telefone = Telefone;
                    respExistente.RelacionamentoAluno = RelacionamentoAluno;
                    respExistente.RM = RM;

                    InsertRespDAO.AtualizarResp(respExistente);
                    MessageBox.Show("Responsável atualizado com sucesso!");
                }
                else
                {
                    // Caso contrário, adiciona como novo
                    var resp = new CadastroResp(IdBiometria, Nome, CPF, Email, Telefone, RelacionamentoAluno, RM);
                    InsertRespDAO.InserirResp(resp);
                    Responsaveis.Add(resp);
                    MessageBox.Show("Responsável cadastrado com sucesso!");
                }

                LimparCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar responsável: {ex.Message}");
            }
        }

        // Verificar se o comando de salvar pode ser executado
        private bool CanExecuteSalvar(object parameter)
        {
            return !string.IsNullOrWhiteSpace(Nome) &&
                   !string.IsNullOrWhiteSpace(CPF) &&
                   !string.IsNullOrWhiteSpace(Email) &&
                   !string.IsNullOrWhiteSpace(IdBiometria) &&
                   !string.IsNullOrWhiteSpace(RelacionamentoAluno) &&
                   !string.IsNullOrWhiteSpace(Telefone) &&
                   RM.HasValue;
        }

        // Carregar os dados do responsável selecionado
        private void CarregarDadosResponsavel()
        {
            if (ResponsavelSelecionado != null)
            {
                Nome = ResponsavelSelecionado.Nome;
                CPF = ResponsavelSelecionado.CPF;
                Email = ResponsavelSelecionado.Email;
                Telefone = ResponsavelSelecionado.Telefone;
                RelacionamentoAluno = ResponsavelSelecionado.RelacionamentoAluno;
                IdBiometria = ResponsavelSelecionado.IdBiometria;
                RM = ResponsavelSelecionado.RM;
            }
        }

        // Limpar campos de entrada
        private void LimparCampos()
        {
            Nome = string.Empty;
            CPF = string.Empty;
            Email = string.Empty;
            Telefone = string.Empty;
            RelacionamentoAluno = string.Empty;
            IdBiometria = string.Empty;
            RM = null;
            ResponsavelSelecionado = null;
        }

        // Implementação do INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
