﻿namespace MiniRedux
{
    public interface IFeature<TState> : IMutable<TState>, IReducible
    {
    }
}
