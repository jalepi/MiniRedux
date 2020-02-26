namespace MiniRedux
{
    public interface IReducible
    {
        void Reduce<TAction>(TAction action);
    }
}
