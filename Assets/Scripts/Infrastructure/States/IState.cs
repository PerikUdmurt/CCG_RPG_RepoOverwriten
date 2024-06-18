namespace CCG.Infrastructure
{
    public interface IState: IExitableState
    {
        void Enter();
    }
}