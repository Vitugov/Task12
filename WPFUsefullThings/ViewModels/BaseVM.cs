﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WPFUsefullThings
{
    public class BaseVM : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual bool Set<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        public virtual bool SetWithAction<T>(ref T field, T value,
            Action refreshAction, [CallerMemberName] string propertyName = null)
        {
            var result = Set(ref field, value, propertyName);
            refreshAction();
            return result;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private bool _Disposed;

        public virtual void Dispose(bool disposing)
        {
            if (!disposing || _Disposed) return;
            _Disposed = true;
        }
    }
}
