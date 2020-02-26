using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniRedux
{
    public class Feature<TState> : Mutable<TState>, IFeature<TState>
    {
        readonly private IEnumerable<IReducer<TState>> reducers;

        public Feature(TState state, IEnumerable<IReducer<TState>> reducers) : base(state) =>
            this.reducers = reducers;

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
    }
}
