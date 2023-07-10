using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;
using System;

public class RealTargetTracker : MonoBehaviour, ITargetTracker
{
    public event Action<object> TargetDetected;
    public event Action<object, Matrix4x4> TargetComputed;
    public event Action<object> TargetLost;

    // Start is called before the first frame update
    void Start()
    {
        WebGLBridge.OnExternalDetect += ExternalDetectedHandler;
        WebGLBridge.OnExternalLost += ExternalLostHandler;
        WebGLBridge.OnExternalCompute += ExternalComputeHandler;
    }

        
    public void EngineStart()
    {
        WebGLBridge.EngineStarted();
    }

    private void ExternalDetectedHandler(int targetID)
    {
        TargetDetected?.Invoke(targetID);
    }

    private void ExternalLostHandler(int targetID)
    {
        TargetLost?.Invoke(targetID);
    }

    private void ExternalComputeHandler(int targetID, Matrix4x4 matrix)
    {
        TargetComputed?.Invoke(targetID);
        TargetComputed?.Invoke(matrix);
    }
}
