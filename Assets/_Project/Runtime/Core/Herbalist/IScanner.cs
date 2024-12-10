namespace _Project.Runtime.Core.Herbalist
{
    internal interface IScanner<out T>
    {
        //Возвращает лист ближайщих
        public T Get();
        public bool IsEmpty { get; }
    }
}
