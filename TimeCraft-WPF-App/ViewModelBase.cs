using System.Collections;
using System.ComponentModel;

namespace TimeCraft_WPF_App
{
    public class ViewModelBase : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        private readonly Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

        public bool HasErrors => _errors.Any(entry => entry.Value.Any());

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public IEnumerable GetErrors(string? propertyName)
        {
            if (propertyName != null && _errors.TryGetValue(propertyName, out var errors))
            {
                return errors;
            }
            return Enumerable.Empty<string>();
        }

        protected void AddError(string propertyName, string error)
        {
            if (!_errors.TryGetValue(propertyName, out var propertyErrors))
            {
                propertyErrors = new List<string>();
                _errors[propertyName] = propertyErrors;
            }

            if (!propertyErrors.Contains(error))
            {
                propertyErrors.Add(error);
                OnErrorsChanged(propertyName);
            }
        }

        protected void ClearErrors(string propertyName)
        {
            if (_errors.TryGetValue(propertyName, out var propertyErrors))
            {
                propertyErrors.Clear();
                OnErrorsChanged(propertyName);
            }
        }

        protected virtual void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            OnPropertyChanged(nameof(HasErrors));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
