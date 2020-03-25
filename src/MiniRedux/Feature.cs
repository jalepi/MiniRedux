using System.Collections.Generic;
using System.Linq;

namespace MiniRedux
{
    public class Feature<TState> : IFeature<TState>
    {
        private readonly IEnumerable<IReducer<TState>> reducers;

        public Feature(TState state, IEnumerable<IReducer<TState>> reducers)
        {
            this.State = state;
            this.reducers = reducers;
        }

        public TState State { get; private set; }

        public event StateChangedHandler<TState> StateChanged = (sender, e) => { };

        public virtual void Reduce<TAction>(TAction action)
        {
            var suitableReducers = reducers.OfType<IReducer<TState, TAction>>();

            foreach (var reducer in suitableReducers)
            {
                var nextState = reducer.Reduce(this.State, action);
                this.SetState(nextState);
            }

        }

        protected virtual bool SetState(TState state)
        {
            var (previous, next) = (this.State, state);

            if (!EqualityComparer<TState>.Default.Equals(previous, next))
            {
                State = next;
                StateChanged.Invoke(this, new StateChangeEventArgs<TState>(previous, next));
                return true;
            }
            return false;
        }
    }
}
