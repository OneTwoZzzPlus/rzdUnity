using System;
using UnityEngine;

namespace Interfaces
{
    public interface IState<T> where T : Enum
    {
        // Start is called before the first frame update
        public T Id {get ;}
        void Enter();
        void Exit();
    }
}
