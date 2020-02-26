using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniRedux
{
    public class Mutable<TState> : IMutable<TState>
    {
        public Mutable(TState state)
        {
            State = state;
            StateChanged = new StateChangedHandler<TState>((sender, e) => { });
        }

        public TState State { get; private set; }

        public event StateChangedHandler<TState> StateChanged;

        protected virtual bool SetState(TState state)
        {
            if (!Object.ReferenceEquals(State, state))
            {
                var (previous, next) = (this.State, state);
                State = next;
                
                StateChanged.Invoke(this, new StateChangeEventArgs<TState>(previous, next));
                return true;
            }
            return false;
        }
    }
}