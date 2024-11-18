using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfBiomEtec.Models;

namespace WpfBiomEtec.ViewModels
{
    public class PresencaViewModel : INotifyPropertyChanged
    {
        private PresencaRepository _repository = new PresencaRepository();
        public ObservableCollection<Presenca> Presencas { get; set; } = new ObservableCollection<Presenca>();

        private Presenca _selectedPresenca;
        public Presenca SelectedPresenca
        {
            get => _selectedPresenca;
            set
            {
                _selectedPresenca = value;
                OnPropertyChanged();
            }
        }

        public PresencaViewModel()
        {
            CarregarDados();
        }

        public void CarregarDados()
        {
            Presencas.Clear();
            foreach (var presenca in _repository.ObterTodos())
            {
                Presencas.Add(presenca);
            }
        }

        public void Salvar()
        {
            if (SelectedPresenca != null)
            {
                _repository.Atualizar(SelectedPresenca);
                CarregarDados();
            }
        }

        public void Excluir()
        {
            if (SelectedPresenca != null)
            {
                _repository.Excluir(SelectedPresenca.Id);
                CarregarDados();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}