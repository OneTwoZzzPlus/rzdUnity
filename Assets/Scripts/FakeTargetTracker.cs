using Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeTargetTracker : MonoBehaviour, ITargetTracker
{
    public event Action<object> TargetDetected;
    public event Action<object> TrgetComputed;
    public event Action<object> TargetLost;

    public void EngineStart()
    {
        Debug.Log("En St");

    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            InvokeTargetDetected(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            InvokeTargetDetected(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            InvokeTargetDetected(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            InvokeTargetDetected(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            InvokeTargetDetected(5);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            InvokeTargetDetected(6);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            InvokeTargetDetected(7);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            InvokeTargetDetected(8);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            InvokeTargetDetected(9);
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            InvokeTargetDetected(0);
        }
    }

    public void InvokeTargetDetected(int targetIndex)
    {
        Debug.Log("k" + targetIndex);
        TargetDetected?.Invoke(targetIndex);
    }
}
