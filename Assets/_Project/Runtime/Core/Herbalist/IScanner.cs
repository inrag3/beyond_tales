using System;
using ObservableCollections;

namespace _Project.Runtime.Core.Herbalist
{
    public interface IScanner<T>
    {
        //Возвращает ближайщего 
        public bool IsEmpty { get; }
        public T Get();
        public T Get(Predicate<T> predicate);
        public void Remove(T intractable);
        public IObservableCollection<T> Components { get; }
    }
}
