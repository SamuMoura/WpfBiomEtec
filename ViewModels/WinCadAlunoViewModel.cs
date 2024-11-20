using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using WpfBiomEtec.Models;
using WpfBiomEtec.DAO;

namespace WpfBiomEtec.ViewModels
{
    public class WinCadAlunoViewModel : INotifyPropertyChanged
    {
        private string _nome;
        private string _cpf;
        private string _idBiometria;
        private string _email;
        private string _telefone;
        private CadastroAluno _alunoSelecionado;

        // Propriedades para Bindings
        public string Nome
        {
            get => _nome;
            set
            {
                if (_nome != value)
                {
                    _nome = value;
                    OnPropertyChanged(nameof(Nome));
                }
            }
        }

        public string CPF
        {
            get => _cpf;
            set
            {
                if (_cpf != value)
                {
                    _cpf = value;
                    OnPropertyChanged(nameof(CPF));
                }
            }
        }

        public string IdBiometria
        {
            get => _idBiometria;
            set
            {
                if (_idBiometria != value)
                {
                    _idBiometria = value;
                    OnPropertyChanged(nameof(IdBiometria));
                }
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged(nameof(Email));
                }
            }
        }

        public string Telefone
        {
            get => _telefone;
            set
            {
                if (_telefone != value)
                {
                    _telefone = value;
                    OnPropertyChanged(nameof(Telefone));
                }
            }
        }

        public ObservableCollection<CadastroAluno> Alunos { get; set; }

        public CadastroAluno AlunoSelecionado
        {
            get => _alunoSelecionado;
            set
            {
                if (_alunoSelecionado != value)
                {
                    _alunoSelecionado = value;
                    OnPropertyChanged(nameof(AlunoSelecionado));

                    // Preenche os campos com os dados do aluno selecionado
                    if (_alunoSelecionado != null)
                    {
                        Nome = _alunoSelecionado.Nome;
                        CPF = _alunoSelecionado.CPF;
                        IdBiometria = _alunoSelecionado.IdBiometria;
                        Email = _alunoSelecionado.Email;
                        Telefone = _alunoSelecionado.Telefone;
                    }
                    else
                    {
                        LimparCampos();
                    }
                }
            }
        }

        // Comandos
        public ICommand SalvarCommand { get; }
        public ICommand ExcluirCommand { get; }
        public ICommand VoltarCommand { get; }

        public WinCadAlunoViewModel()
        {
            // Inicializa os comandos
            SalvarCommand = new RelayCommand(SalvarAluno, CanExecuteSalvar);
            ExcluirCommand = new RelayCommand(ExcluirAluno, CanExecuteExcluir);
            VoltarCommand = new RelayCommand(Voltar);

            // Carregar a lista de alunos
            Alunos = new ObservableCollection<CadastroAluno>(InsertAlunoDAO.ListarAlunos());
        }

        // Valida se o botão Salvar pode ser executado
        private bool CanExecuteSalvar(object parameter)
        {
            return !string.IsNullOrWhiteSpace(Nome) &&
                   !string.IsNullOrWhiteSpace(CPF) &&
                   !string.IsNullOrWhiteSpace(Email) &&
                   !string.IsNullOrWhiteSpace(Telefone);
        }

        private void SalvarAluno(object parameter)
        {
            MessageBox.Show("SalvarAluno disparado!");
            try
            {
                if (AlunoSelecionado == null || string.IsNullOrWhiteSpace(AlunoSelecionado.RM.ToString()))
                {
                    // Inserção
                    var novoAluno = new CadastroAluno(Nome, CPF, IdBiometria, Email, Telefone);
                    InsertAlunoDAO.InserirAluno(novoAluno);
                    MessageBox.Show("Aluno cadastrado com sucesso!");
                }
                else
                {
                    // Atualização
                    AlunoSelecionado.Nome = Nome;
                    AlunoSelecionado.CPF = CPF;
                    AlunoSelecionado.IdBiometria = IdBiometria;
                    AlunoSelecionado.Email = Email;
                    AlunoSelecionado.Telefone = Telefone;

                    InsertAlunoDAO.AtualizarAluno(AlunoSelecionado);
                    MessageBox.Show("Dados do aluno atualizados com sucesso!");
                }

                LimparCampos();
                CarregarAlunos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar aluno: {ex.Message}");
            }
        }


        // Valida se o botão Excluir pode ser executado
        private bool CanExecuteExcluir(object parameter)
        {
            return AlunoSelecionado != null;
        }

        private void ExcluirAluno(object parameter)
        {
            if (AlunoSelecionado != null)
            {
                var resultado = MessageBox.Show(
                    "Tem certeza que deseja excluir o aluno selecionado?",
                    "Confirmação de Exclusão",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (resultado == MessageBoxResult.Yes)
                {
                    try
                    {
                        InsertAlunoDAO.ExcluirAluno(AlunoSelecionado.RM);
                        MessageBox.Show("Aluno excluído com sucesso!");
                        Alunos.Remove(AlunoSelecionado);
                        LimparCampos();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao excluir aluno: {ex.Message}");
                    }
                }
            }
        }


        private void Voltar(object parameter)
        {
            var menuWindow = new WinMenu();
            menuWindow.Show();
            Application.Current.Windows[0]?.Close();
        }

        private void LimparCampos()
        {
            Nome = string.Empty;
            CPF = string.Empty;
            IdBiometria = string.Empty;
            Email = string.Empty;
            Telefone = string.Empty;
        }

        private void CarregarAlunos()
        {
            Alunos.Clear();
            var alunos = InsertAlunoDAO.ListarAlunos();
            foreach (var aluno in alunos)
            {
                Alunos.Add(aluno);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    // Implementação de RelayCommand para os comandos
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute?.Invoke(parameter) ?? true;

        public void Execute(object parameter) => _execute(parameter);

        public event EventHandler CanExecuteChanged;
    }
}
