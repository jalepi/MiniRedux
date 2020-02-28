using System.Threading;
using System.Threading.Tasks;

namespace MiniRedux
{
    public interface IDispatcher
    {
        Task Dispatch<TAction>(TAction action);
        Task Dispatch<TAction>(TAction action, CancellationToken cancellationToken = default);
    }
}
