namespace CCG.Infrastructure
{
    public interface IPayloadedState<TPayload>: IExitableState
    {
        void Enter(TPayload payload);
    }
}