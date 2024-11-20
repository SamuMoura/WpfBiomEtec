using System.Windows;
using System.Windows.Input;
using WpfBiomEtec.Views;
using WpfBiomEtec.Models;

namespace WpfBiomEtec.ViewModels
{
    public class WinMenuViewModel
    {
        public ICommand OpenCadRespCommand { get; }
        public ICommand OpenCadAlunoCommand { get; }
        public ICommand OpenCadUsuarioCommand { get; }
        public ICommand OpenDSCommand { get; }
        public ICommand OpenMACommand { get; }
        public ICommand OpenMNCommand { get; }
        public ICommand OpenPassagemCommand { get; }
        public ICommand LogoffCommand { get; }

        public WinMenuViewModel()
        {
            OpenCadRespCommand = new RelayCommand(OpenCadResp);
            OpenCadAlunoCommand = new RelayCommand(OpenCadAluno);
            OpenCadUsuarioCommand = new RelayCommand(OpenCadUsuario);
            OpenDSCommand = new RelayCommand(OpenDS);
            OpenMACommand = new RelayCommand(OpenMA);
            OpenMNCommand = new RelayCommand(OpenMN);
            LogoffCommand = new RelayCommand(Logoff);
        }

        private void OpenCadResp(object obj)
        {
            // Cria a nova janela antes de fechar a atual
            var window = new WinCadResp();
            window.Show();

            if (obj is Window currentWindow)
            {
                currentWindow.Close(); // Fecha a janela atual (WinMenu)
            }
        }

        private void OpenCadAluno(object obj)
        {
            var window = new WinCadAluno();
            window.Show();

            if (obj is Window currentWindow)
            {
                currentWindow.Close();
            }
        }

        private void OpenCadUsuario(object obj)
        {
            var window = new WinCadUsuario();
            window.Show();

            if (obj is Window currentWindow)
            {
                currentWindow.Close();
            }
        }

        private void OpenDS(object obj)
        {
            var window = new WinDS();
            window.Show();

            if (obj is Window currentWindow)
            {
                currentWindow.Close();
            }
        }

        private void OpenMA(object obj)
        {
            var window = new WinMA();
            window.Show();

            if (obj is Window currentWindow)
            {
                currentWindow.Close();
            }
        }

        private void OpenMN(object obj)
        {
            var window = new WinMN();
            window.Show();

            if (obj is Window currentWindow)
            {
                currentWindow.Close();
            }
        }

        private void Logoff(object obj)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();

            if (obj is Window currentWindow)
            {
                currentWindow.Close(); // Fecha a janela atual (WinMenu)
            }
        }



        private void CloseCurrentWindow(Window currentWindow)
        {
            if (currentWindow != null)
            {
                currentWindow.Close();
            }
        }
    }
}
