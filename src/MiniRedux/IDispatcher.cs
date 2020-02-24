using System.Threading.Tasks;

namespace MiniRedux
{
    public interface IDispatcher
    {
        Task Dispatch<TAction>(TAction action);
    }
}
