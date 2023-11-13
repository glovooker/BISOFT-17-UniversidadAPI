namespace UniversidadAPI.Iterador
{
    public interface IIterableCollection<T>
    {
        IIterator<T> CreateIterator();
    }

}
