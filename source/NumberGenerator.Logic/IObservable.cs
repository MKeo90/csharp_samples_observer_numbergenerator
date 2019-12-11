namespace NumberGenerator.Logic
{
    public interface IObservable
    {
        void Attach(IObserver observer);
        void Detach(IObserver observer);

        void NotifyObservers(int number);
        bool IsObserverRegistrated(IObserver observer);
    }
}