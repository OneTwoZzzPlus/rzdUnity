using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IStateMachine<T> where T : Enum
{
    void ChangeState(T desiredSteted);
    void RevertState();
}