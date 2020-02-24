using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiniRedux
{
    public abstract class Reducer<TState, TAction> : IReducer<TState, TAction>
    {
        public abstract TState Reduce(TState state, TAction action);

        public TState Reduce<TSome>(TState state, TSome some)
        {
            return some is TAction action ? Reduce(state, action) : state;
        }
    }
}
