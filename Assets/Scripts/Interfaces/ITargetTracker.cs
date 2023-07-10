using System;
using UnityEngine;
using Interfaces;

namespace Interfaces
{
    public interface ITargetTracker
    {
        public event Action<object> TargetDetected;
        public event Action<object, Matrix4x4> TargetComputed;
        public event Action<object> TargetLost;

        public void EngineStart();
    }


}

