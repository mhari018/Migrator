namespace MigratorABC.Events
{
    public class BaseEvent<T>
    {
        public string SyncName { get; set; } = default!;
        public string DocumentNumber { get; set; } = default!;
        public T? SyncData { get; set; }
    }
}
