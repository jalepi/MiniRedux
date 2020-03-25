namespace MiniRedux
{
    public abstract class Reducer<TState, TAction> : IReducer<TState, TAction>
    {
        public abstract TState Reduce(TState state, TAction action);

        public virtual TState Reduce<TSome>(TState state, TSome some) =>
            some is TAction action 
                ? Reduce(state, action) 
                : state;
    }
}
