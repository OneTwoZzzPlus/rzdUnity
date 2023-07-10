using System;
using Interfaces;
using UnityEngine;

public class FakeTargetTracker : MonoBehaviour, ITargetTracker
{
    public event Action<int> TargetDetected;
    public event Action<int, Matrix4x4> TargetComputed;
    public event Action<int> TargetLost;

    public void EngineStart()
    {
        Debug.Log("ENGINE STARTED");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0)) InvokeTargetDetect(0);
        if (Input.GetKeyDown(KeyCode.Alpha1)) InvokeTargetDetect(1);
        if (Input.GetKeyDown(KeyCode.Alpha2)) InvokeTargetDetect(2);
        if (Input.GetKeyDown(KeyCode.Alpha3)) InvokeTargetDetect(3);
        if (Input.GetKeyDown(KeyCode.Alpha4)) InvokeTargetDetect(4);
        if (Input.GetKeyDown(KeyCode.Alpha5)) InvokeTargetDetect(5);
        if (Input.GetKeyDown(KeyCode.Alpha6)) InvokeTargetDetect(6);
        if (Input.GetKeyDown(KeyCode.Alpha7)) InvokeTargetDetect(7);
        if (Input.GetKeyDown(KeyCode.Alpha8)) InvokeTargetDetect(8);
        if (Input.GetKeyDown(KeyCode.Alpha9)) InvokeTargetDetect(9);

        if (Input.GetKeyUp(KeyCode.Alpha0)) InvokeTargetLost(0);
        if (Input.GetKeyUp(KeyCode.Alpha1)) InvokeTargetLost(1);
        if (Input.GetKeyUp(KeyCode.Alpha2)) InvokeTargetLost(2);
        if (Input.GetKeyUp(KeyCode.Alpha3)) InvokeTargetLost(3);
        if (Input.GetKeyUp(KeyCode.Alpha4)) InvokeTargetLost(4);
        if (Input.GetKeyUp(KeyCode.Alpha5)) InvokeTargetLost(5);
        if (Input.GetKeyUp(KeyCode.Alpha6)) InvokeTargetLost(6);
        if (Input.GetKeyUp(KeyCode.Alpha7)) InvokeTargetLost(7);
        if (Input.GetKeyUp(KeyCode.Alpha8)) InvokeTargetLost(8);
        if (Input.GetKeyUp(KeyCode.Alpha9)) InvokeTargetLost(9);

    }

    private void InvokeTargetDetect(int targetIndex) 
    {
        Debug.Log($"Key pressed {targetIndex}");
        TargetDetected?.Invoke(targetIndex);
    }

    private void InvokeTargetLost(int targetIndex)
    {
        Debug.Log($"Key lost {targetIndex}");
        TargetLost?.Invoke(targetIndex);
    }


}
