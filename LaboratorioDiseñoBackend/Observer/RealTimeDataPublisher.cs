namespace UniversidadAPI.Observer
{
    public class RealTimeDataPublisher : ISubject
    {
        private readonly List<IObserver> _observers = new List<IObserver>();

        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void Notify(string message)
        {
            foreach (var observer in _observers)
            {
                observer.Update(message);
            }
        }

        // Método que se llama para actualizar datos
        public void UpdateData(string newData)
        {
            // Aquí iría la lógica para actualizar los datos en la base de datos
            // ...

            // Después de actualizar los datos, notificar a los observers
            Notify(newData);
        }
    }

}
