namespace MiniRedux
{
    public interface IReducer<TState>
    {
        TState Reduce<TSome>(TState state, TSome action);
    }

    public interface IReducer<TState, in TAction> : IReducer<TState>
    {
        TState Reduce(TState state, TAction action);
    }
}
