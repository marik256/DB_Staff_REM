namespace DB_Staff_REM.EventHub
{
    public interface IEventHub
    {
        void Publish<T>(T message);

        void Subscribe<TMessage, THandler>(THandler handler) where THandler : IEventHandler<TMessage>;
    }
}
