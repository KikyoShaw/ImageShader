using System.Collections.Generic;
using System.ComponentModel;

namespace ImageShader.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected internal virtual void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //注意：值发生变化的时候，才抛通知的
        public bool Set<T>(string propertyName, ref T field, T newValue = default(T))
        {
            try
            {
                if (EqualityComparer<T>.Default.Equals(field, newValue) == false)
                {
                    field = newValue;
                    RaisePropertyChanged(propertyName);
                    return true;
                }
            }
            catch
            {
                // ignored
            }

            return false;
        }
    }
}
