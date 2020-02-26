using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniRedux
{
    public class Store : IStore, IDispatcher
    {
        readonly private IEnumerable<IReducible> features;
        readonly private IEnumerable<IEffect> effects;

        public Store(IEnumerable<IReducible> features) : this(features, new IEffect[] { }) { }

        public Store(IEnumerable<IReducible> features, IEnumerable<IEffect> effects) =>
            (this.features, this.effects) = (features, effects);

        virtual public async Task Dispatch<TAction>(TAction action)
        {
            foreach (var feature in this.features)
            {
                feature.Reduce(action);
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
