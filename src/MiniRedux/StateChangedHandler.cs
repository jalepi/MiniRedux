namespace MiniRedux
{
    public delegate void StateChangedHandler<in TState>(object sender, IStateChangeEventArgs<TState> stateChange);
}