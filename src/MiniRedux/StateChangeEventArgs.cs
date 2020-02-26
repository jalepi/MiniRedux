namespace MiniRedux
{
    public class StateChangeEventArgs<TState> : IStateChangeEventArgs<TState>
    {
        public StateChangeEventArgs(TState previous, TState next)
        {
            this.Previous = previous;
            this.Next = next;
        }

        public TState Previous { get; }
        public TState Next { get; }
    }
}
