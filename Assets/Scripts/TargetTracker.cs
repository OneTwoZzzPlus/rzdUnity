using System;
using Interfaces;
using UnityEngine;

public class TargetTracker : MonoBehaviour, ITargetTracker
{
    public event Action<int> TargetDetected;
    public event Action<int, Matrix4x4> TargetMatrixComputed;
    public event Action<int> TargetLost;
    
    public void StartTracking()
    {
        WebGLBridge.EngineStarted();
    }

    private void Awake()
    {
        WebGLBridge.OnExternalDetect += OnExternalDetectHandler;
        WebGLBridge.OnExternalCompute += OnExternalComputeHandler;
        WebGLBridge.OnExternalLost += OnExternalLostHandler;
    }

    private void OnExternalLostHandler(int index)
    {
        TargetLost?.Invoke(index);
    }

    private void OnExternalComputeHandler(int index, Matrix4x4 transformMatrix)
    {
        TargetMatrixComputed?.Invoke(index, transformMatrix);
    }

    private void OnExternalDetectHandler(int index)
    {
        TargetDetected?.Invoke(index);
    }
}