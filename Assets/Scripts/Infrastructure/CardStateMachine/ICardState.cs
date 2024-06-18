using CCG.Infrastructure;

namespace CCG.Gameplay
{
    public interface ICardState: IExitableState
    {
        void Enter();
    }

    public interface ICardStatePayloaded<TPayload> : IExitableState
    {
        void Enter(TPayload payload);
    }

}
