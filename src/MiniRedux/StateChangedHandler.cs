namespace MiniRedux
{
    public delegate void StateChangedHandler<in T>(object sender, T args);
}
