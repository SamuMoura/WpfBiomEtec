using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using WpfBiomEtec.DAO;
using WpfBiomEtec.Models;

namespace WpfBiomEtec.ViewModels
{
    public class WinCadUsuarioViewModel : INotifyPropertyChanged
    {
        private string _usuario;
        private string _senha;
        private string _permissao;

        // Propriedades para Bindings
        public string Usuario
        {
            get => _usuario;
            set
            {
                _usuario = value;
                OnPropertyChanged(nameof(Usuario));
            }
        }

        public string Senha
        {
            get => _senha;
            set
            {
                _senha = value;
                OnPropertyChanged(nameof(Senha));
            }
        }

        public string Permissao
        {
            get => _permissao;
            set
            {
                _permissao = value;
                OnPropertyChanged(nameof(Permissao));
            }
        }

        // Comandos para os botões
        public ICommand CadastrarCommand { get; }
        public ICommand VoltarCommand { get; }

        // Construtor
        public WinCadUsuarioViewModel()
        {
            // Inicializa os comandos
            CadastrarCommand = new RelayCommand(CadastrarUsuario, CanExecuteCadastrar);
            VoltarCommand = new RelayCommand(Voltar);
        }

        // Valida se o botão de cadastrar pode ser executado
        private bool CanExecuteCadastrar(object parameter)
        {
            return !string.IsNullOrWhiteSpace(Usuario) &&
                   !string.IsNullOrWhiteSpace(Senha) &&
                   !string.IsNullOrWhiteSpace(Permissao);
        }

        private void CadastrarUsuario(object parameter)
        {
            try
            {
                var user = new CadastroUsuario(Usuario, Senha, Permissao);
                InsertUsuarioDAO.InserirUsuario(user);
                MessageBox.Show("Responsável cadastrado com sucesso!");
                LimparCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao cadastrar usuário: {ex.Message}");
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
            Usuario = string.Empty;
            Senha = string.Empty;
            Permissao = string.Empty;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
