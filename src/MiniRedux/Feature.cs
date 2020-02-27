using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniRedux
{
    public class Feature<TState> : IFeature<TState>
    {
        readonly private IEnumerable<IReducer<TState>> reducers;

        public Feature(TState state, IEnumerable<IReducer<TState>> reducers) =>
            (this.State, this.reducers) = (state, reducers);

        public TState State { get; private set; }

        public event StateChangedHandler<TState> StateChanged = (sender, e) => { };

        public void Reduce<TAction>(TAction action)
        {
            var currentState = this.State;

            foreach (var reducer in reducers)
            {
                if (reducer is IReducer<TState, TAction> r)
                {
                    currentState = r.Reduce(currentState, action);
                }
            }

            this.SetState(currentState);
        }

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
