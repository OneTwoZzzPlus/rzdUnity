using System;
using UnityEngine;

namespace Interfaces
{
    public interface ITargetTracker
    {
        public event Action<int> TargetDetected;
        public event Action<int, Matrix4x4> TargetComputed;
        public event Action<int> TargetLost;

        public void EngineStart();
    }
}
