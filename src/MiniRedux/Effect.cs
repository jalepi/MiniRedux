using System.Threading;
using System.Threading.Tasks;

namespace MiniRedux
{
    public abstract class Effect<TAction> : IEffect<TAction>
    {
        public virtual Task React(TAction action, IDispatcher dispatcher) => React(action, dispatcher, default);
        public abstract Task React(TAction action, IDispatcher dispatcher, CancellationToken cancellationToken);

        public virtual Task React<TSome>(TSome some, IDispatcher dispatcher) => React(some, dispatcher, default);
        public virtual Task React<TSome>(TSome some, IDispatcher dispatcher, CancellationToken cancellationToken = default) =>
            some is TAction action ? React(action, dispatcher, cancellationToken) : Task.CompletedTask;
    }
}
