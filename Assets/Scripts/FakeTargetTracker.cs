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
        if (Input.GetKeyDown(KeyCode.Q)) InvokeTargetDetect(10);
        if (Input.GetKeyDown(KeyCode.W)) InvokeTargetDetect(11);
        if (Input.GetKeyDown(KeyCode.E)) InvokeTargetDetect(12);
        if (Input.GetKeyDown(KeyCode.R)) InvokeTargetDetect(13);
        if (Input.GetKeyDown(KeyCode.T)) InvokeTargetDetect(14);
        if (Input.GetKeyDown(KeyCode.Y)) InvokeTargetDetect(15);
        if (Input.GetKeyDown(KeyCode.U)) InvokeTargetDetect(16);
        if (Input.GetKeyDown(KeyCode.I)) InvokeTargetDetect(17);
        if (Input.GetKeyDown(KeyCode.O)) InvokeTargetDetect(18);
        if (Input.GetKeyDown(KeyCode.P)) InvokeTargetDetect(19);
        if (Input.GetKeyDown(KeyCode.A)) InvokeTargetDetect(20);
        if (Input.GetKeyDown(KeyCode.S)) InvokeTargetDetect(21);
        if (Input.GetKeyDown(KeyCode.D)) InvokeTargetDetect(22);
        if (Input.GetKeyDown(KeyCode.F)) InvokeTargetDetect(23);
        if (Input.GetKeyDown(KeyCode.G)) InvokeTargetDetect(24);
        if (Input.GetKeyDown(KeyCode.H)) InvokeTargetDetect(25);
        if (Input.GetKeyDown(KeyCode.J)) InvokeTargetDetect(26);
        if (Input.GetKeyDown(KeyCode.K)) InvokeTargetDetect(27);
        if (Input.GetKeyDown(KeyCode.L)) InvokeTargetDetect(28);
        if (Input.GetKeyDown(KeyCode.Z)) InvokeTargetDetect(29);
        if (Input.GetKeyDown(KeyCode.X)) InvokeTargetDetect(30);
        if (Input.GetKeyDown(KeyCode.C)) InvokeTargetDetect(31);
        if (Input.GetKeyDown(KeyCode.V)) InvokeTargetDetect(32);


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
        if (Input.GetKeyUp(KeyCode.Q)) InvokeTargetLost(10);
        if (Input.GetKeyUp(KeyCode.W)) InvokeTargetLost(11);
        if (Input.GetKeyUp(KeyCode.E)) InvokeTargetLost(12);
        if (Input.GetKeyUp(KeyCode.R)) InvokeTargetLost(13);
        if (Input.GetKeyUp(KeyCode.T)) InvokeTargetLost(14);
        if (Input.GetKeyUp(KeyCode.Y)) InvokeTargetLost(15);
        if (Input.GetKeyUp(KeyCode.U)) InvokeTargetLost(16);
        if (Input.GetKeyUp(KeyCode.I)) InvokeTargetLost(17);
        if (Input.GetKeyUp(KeyCode.O)) InvokeTargetLost(18);
        if (Input.GetKeyUp(KeyCode.P)) InvokeTargetLost(19);
        if (Input.GetKeyUp(KeyCode.A)) InvokeTargetLost(20);
        if (Input.GetKeyUp(KeyCode.S)) InvokeTargetLost(21);
        if (Input.GetKeyUp(KeyCode.D)) InvokeTargetLost(22);
        if (Input.GetKeyUp(KeyCode.F)) InvokeTargetLost(23);
        if (Input.GetKeyUp(KeyCode.G)) InvokeTargetLost(24);
        if (Input.GetKeyUp(KeyCode.H)) InvokeTargetLost(25);
        if (Input.GetKeyUp(KeyCode.J)) InvokeTargetLost(26);
        if (Input.GetKeyUp(KeyCode.K)) InvokeTargetLost(27);
        if (Input.GetKeyUp(KeyCode.L)) InvokeTargetLost(28);
        if (Input.GetKeyUp(KeyCode.Z)) InvokeTargetLost(29);
        if (Input.GetKeyUp(KeyCode.X)) InvokeTargetLost(30);
        if (Input.GetKeyUp(KeyCode.C)) InvokeTargetLost(31);
        if (Input.GetKeyUp(KeyCode.V)) InvokeTargetLost(32);
        if (Input.GetKeyDown(KeyCode.B)) InvokeTargetCompute(32, new Matrix4x4());
    }

    private void InvokeTargetDetect(int targetIndex) 
    {
        Debug.Log($"Fake detect {targetIndex}");
        TargetDetected?.Invoke(targetIndex);
    }

    private void InvokeTargetLost(int targetIndex)
    {
        Debug.Log($"Fake lost {targetIndex}");
        TargetLost?.Invoke(targetIndex);
    }

    private void InvokeTargetCompute(int targetIndex, Matrix4x4 matrix)
    {
        TargetComputed?.Invoke(targetIndex, matrix);
    }


}
