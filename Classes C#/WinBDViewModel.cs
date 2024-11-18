    using System.Collections.ObjectModel;
    using System.Windows.Input;

    namespace WpfBiomEtec
    {
        public class WinBDViewModel
        {
            // Propriedades
            public ObservableCollection<Aluno> DataList { get; set; }
            public Aluno SelectedData { get; set; }
            public Aluno EditableData { get; set; }
            public bool IsEditing { get; set; }

            // Comandos
            public ICommand EditCommand { get; }
            public ICommand DeleteCommand { get; }
            public ICommand NewCommand { get; }
            public ICommand SaveCommand { get; }
            public ICommand CancelCommand { get; }

            // Construtor
            public WinBDViewModel()
            {
                DataList = new ObservableCollection<Aluno>();
                LoadData();
                EditCommand = new RelayCommand(EditSelected);
                DeleteCommand = new RelayCommand(DeleteSelected);
                NewCommand = new RelayCommand(NewRecord);
                SaveCommand = new RelayCommand(SaveChanges);
                CancelCommand = new RelayCommand(CancelEdit);
            }

            // Métodos para carregar dados, editar, deletar, etc.
            private void LoadData()
            {
                // Código para carregar dados do banco usando a ConnectionFactory e DAO
            }

            private void EditSelected()
            {
                IsEditing = true;
                EditableData = SelectedData;
            }

            private void DeleteSelected()
            {
                // Código para deletar o item selecionado do banco de dados
            }

            private void NewRecord()
            {
                IsEditing = true;
                EditableData = new Aluno();
            }

            private void SaveChanges()
            {
                // Código para salvar as mudanças no banco de dados
            }

            private void CancelEdit()
            {
                IsEditing = false;
                EditableData = null;
            }
        }
    }
