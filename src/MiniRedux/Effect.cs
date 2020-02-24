﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiniRedux
{
    public abstract class Effect<TAction> : IEffect<TAction>
    {
        public abstract Task React(TAction action, IDispatcher dispatcher);

        virtual public Task React<TSome>(TSome some, IDispatcher dispatcher)
        {
            return some is TAction action ? React(action, dispatcher) : Task.CompletedTask;
        }
    }
}