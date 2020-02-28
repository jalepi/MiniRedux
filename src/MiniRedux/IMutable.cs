namespace MiniRedux
{
    public interface IMutable<out TState>
    {
        TState State { get; }
        event StateChangedHandler<TState> StateChanged;
    }
}
