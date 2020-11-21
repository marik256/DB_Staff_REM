namespace DB_Staff_REM.EventHub
{
    public interface IEventHandler<T>
    {
        void Handle(T message);
    }
}
