using System;
using System.Windows.Input;

namespace WpfBiomEtec.Models
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _executeWithParameter;
        private readonly Action _executeWithoutParameter;
        private readonly Predicate<object> _canExecute;

        // Construtor para comandos com parâmetro
        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _executeWithParameter = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        // Construtor para comandos sem parâmetro
        public RelayCommand(Action execute, Predicate<object> canExecute = null)
        {
            _executeWithoutParameter = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        // Verifica se o comando pode ser executado
        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke(parameter) ?? true;
        }

        // Executa o comando
        public void Execute(object parameter)
        {
            if (parameter != null && _executeWithParameter != null)
            {
                _executeWithParameter(parameter);
            }
            else
            {
                _executeWithoutParameter?.Invoke();
            }
        }

        // Evento para notificar mudanças no estado do comando
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
