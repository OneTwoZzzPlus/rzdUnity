using System;
using Interfaces;
using UnityEngine;

namespace Tests
{
    public class FakeTargetTracker :  ITargetTracker
    {
        public event Action<int> TargetDetected;
        public event Action<int, Matrix4x4> TargetMatrixComputed;
        public event Action<int> TargetLost;
        
        public void StartTracking()
        {
            
        }

        public void OnTargetDetected(int targetId)
        {
            TargetDetected?.Invoke(targetId);
        }

        protected virtual void OnTargetMatrixComputed(int targetId, Matrix4x4 matrix)
        {
            TargetMatrixComputed?.Invoke(targetId, matrix);
        }

        protected virtual void OnTargetLost(int targetId)
        {
            TargetLost?.Invoke(targetId);
        }
        
    }
}