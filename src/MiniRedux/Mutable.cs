using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniRedux
{
    public class Mutable<TState> : IMutable<TState>
    {
        private readonly IList<Func<TState, TState, Task>> subscribers = new List<Func<TState, TState, Task>>();
        public Mutable(TState state)
        {
            State = state;
            StateChanged = new StateChangedHandler<TState>((sender, e) => { });
        }

        public TState State { get; private set; }

        public event StateChangedHandler<TState> StateChanged;

        delegate TResult Fn<in T, out TResult>(T t);
        public IDisposable Subscribe(Func<TState, TState, Task> handler)
        {
            lock (subscribers) { subscribers.Add(handler); }
            return new DisposeInvoker(() =>
            {
                lock (subscribers) { subscribers.Remove(handler); }
            });
        }

        protected virtual bool SetState(TState state)
        {
            if (!Object.ReferenceEquals(State, state))
            {
                State = state;
                return true;
            }
            return false;
        }

        protected virtual async Task NotifySubscribers(TState previousState, TState currentState)
        {
            foreach (var handler in subscribers)
            {
                await handler(previousState, currentState);
            }
        }

        private class DisposeInvoker : IDisposable
        {
            private readonly Action action;
            public DisposeInvoker(Action action) => this.action = action;
            public void Dispose() => this.action();
        }
    }
}