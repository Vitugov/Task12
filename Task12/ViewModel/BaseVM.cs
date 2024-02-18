using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Task12.ViewModel
{
    public class BaseVM : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected virtual bool SetWithFilter<T>(ref T field, T value,
            Action refreshFilter, [CallerMemberName] string propertyName = null)
        {
            var result = Set(ref field, value, propertyName);
            refreshFilter();
            return result;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private bool _Disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing || _Disposed) return;
            _Disposed = true;
        }
    }
}
