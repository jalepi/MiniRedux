using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MiniRedux
{
    public class Store : IStore, IDispatcher
    {
        private readonly IEnumerable<IReducible> features;
        private readonly IEnumerable<IEffect> effects;

        public Store(IEnumerable<IReducible> features) : this(features, new IEffect[] { }) { }

        public Store(IEnumerable<IReducible> features, IEnumerable<IEffect> effects) =>
            (this.features, this.effects) = (features, effects);

        public virtual async Task Dispatch<TAction>(TAction action) => await Dispatch(action, default);

        public virtual async Task Dispatch<TAction>(TAction action, CancellationToken cancellationToken)
        {
            foreach (var feature in this.features)
            {
                feature.Reduce(action);
            }

            foreach (var effect in this.effects)
            {
                if (effect is IEffect<TAction> fx)
                {
                    await fx.React(action, this, cancellationToken);
                }
            }
        }
    }
}
