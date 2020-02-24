using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniRedux
{
    public class Store : IStore, IDispatcher
    {
        readonly private IEnumerable<IDispatcher> features;
        readonly private IEnumerable<IEffect> effects;

        public Store(IEnumerable<IDispatcher> features) : this(features, new IEffect[] { }) { }

        public Store(IEnumerable<IDispatcher> features, IEnumerable<IEffect> effects) =>
            (this.features, this.effects) = (features, effects);

        virtual public async Task Dispatch<TAction>(TAction action)
        {
            foreach (var feature in this.features)
            {
                await feature.Dispatch(action);
            }

            foreach (var effect in this.effects)
            {
                if (effect is IEffect<TAction> fx)
                {
                    await fx.React(action, this);
                }
            }
        }
    }
}
