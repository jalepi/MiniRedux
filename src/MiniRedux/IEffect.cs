using System.Threading;
using System.Threading.Tasks;

namespace MiniRedux
{
    public interface IEffect
    {
        Task React<TSome>(TSome some, IDispatcher dispatcher);
        Task React<TSome>(TSome some, IDispatcher dispatcher, CancellationToken cancellationToken = default);
    }

    public interface IEffect<in TAction> : IEffect
    {
        Task React(TAction action, IDispatcher dispatcher);
        Task React(TAction action, IDispatcher dispatcher, CancellationToken cancellationToken = default);
    }
}
