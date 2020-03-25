using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MiniRedux
{
    public class Store : IStore, IDispatcher
    {
        private readonly IEnumerable<IReducible> features;
        private readonly IEnumerable<IEffect> effects;

        public Store(IEnumerable<IReducible> features)
        {
            this.features = features;
            this.effects = new IEffect[] { };
        }

        public Store(IEnumerable<IReducible> features, IEnumerable<IEffect> effects)
        {
            this.features = features;
            this.effects = effects;
        }

        public virtual async Task Dispatch<TAction>(TAction action) => await Dispatch(action, default);

        public virtual async Task Dispatch<TAction>(TAction action, CancellationToken cancellationToken)
        {
            foreach (var feature in this.features)
            {
                feature.Reduce(action);
            }

            var suitableEffects = this.effects.OfType<IEffect<TAction>>();

            foreach (var effect in suitableEffects)
            {
                await effect.React(action, this, cancellationToken);
            }
        }
    }
}
