using System;
using System.Reflection;
using Interfaces;
using Main;
using UnityEngine;

namespace DefaultNamespace
{
    public class RealTargetTracker : MonoBehaviour, ITargetTracker
    {
        public event Action<int> TargetDetected;
        public event Action<int, Matrix4x4> TargetComputed;
        public event Action<int> TargetLost;

        private void Start()
        {
            WebGLBridge.OnExternalDetect += OnExternalDetect;
            WebGLBridge.OnExternalCompute += OnExternalCompute;
            WebGLBridge.OnExternalLost += OnExternalLost;
        }

        public void EngineStart()
        {
            WebGLBridge.EngineStarted();
            Debug.Log("ENGINE STARTED");
        }

        private void OnExternalDetect(int index)
        {
            Debug.Log("Detected " + index);
            TargetDetected?.Invoke(index);

        }

        private void OnExternalCompute(int index, Matrix4x4 matrix)
        {
            TargetComputed?.Invoke(index, matrix);
        }

        private void OnExternalLost(int index)
        {             
            Debug.Log("Lost " + index);
            TargetLost?.Invoke(index);
        }


    }
}