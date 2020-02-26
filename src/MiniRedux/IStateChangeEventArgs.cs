namespace MiniRedux
{
    public interface IStateChangeEventArgs<out TState>
    {
        TState Previous { get; }
        TState Next { get; }
    }
}
