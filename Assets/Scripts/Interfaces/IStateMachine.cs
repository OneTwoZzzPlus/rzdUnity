using System;
using UnityEngine;

namespace Interfaces
{
    public interface IStateMachine<T> where T : Enum
    {
        void ChangeState(T desiredStateId);
    }
}
