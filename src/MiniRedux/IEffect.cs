using System.Threading.Tasks;

namespace MiniRedux
{
    public interface IEffect
    {
        Task React<TSome>(TSome some, IDispatcher dispatcher);
    }

    public interface IEffect<in TAction> : IEffect
    {
        Task React(TAction action, IDispatcher dispatcher);
    }
}
