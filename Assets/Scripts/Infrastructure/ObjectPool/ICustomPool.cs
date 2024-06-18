namespace CCG.Infrastructure
{
    public interface ICustomPool
    {
        void OnCreated();
        void OnReceipt();
        void OnReleased();
    }
}