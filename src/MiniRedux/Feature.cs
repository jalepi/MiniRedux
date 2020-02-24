using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniRedux
{
    public class Feature<TState> : Mutable<TState>, IFeature<TState>
    {
        readonly private IEnumerable<IReducer<TState>> reducers;

        public Feature(TState state, IEnumerable<IReducer<TState>> reducers) : base(state) =>
            this.reducers = reducers;

        public async Task Dispatch<TAction>(TAction action)
        {
            var previousState = this.State;
            var currentState = this.State;

            foreach (var reducer in reducers)
            {
                if (reducer is IReducer<TState, TAction> r)
                {
                    currentState = r.Reduce(currentState, action);
                }
            }

            if (this.SetState(currentState))
            {
                await NotifySubscribers(previousState, currentState);
            }
        }
    }
}
