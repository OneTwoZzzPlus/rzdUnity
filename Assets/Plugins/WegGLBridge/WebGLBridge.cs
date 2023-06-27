using System;
using System.Runtime.InteropServices;
using UnityEngine;

public static class WebGLBridge
{
    public static Action<int> OnExternalDetect { get; set; }
    public static Action<int, Matrix4x4> OnExternalCompute { get; set; }
    public static Action<int> OnExternalLost { get; set; }

    [DllImport("__Internal")]
    private static extern void OnStartEngine();

    [DllImport("__Internal")]
    private static extern void OnStartAudio();

    [DllImport("__Internal")]
    private static extern void OnStopAudio();

    [DllImport("__Internal")]
    private static extern void OnStartAr();

    [DllImport("__Internal")]
    private static extern void OnStopAr();

    [DllImport("__Internal")]
    private static extern void OnCameraAllowed();

    [DllImport("__Internal")]
    private static extern void OnMute();

    [DllImport("__Internal")]
    private static extern void OnUnmute();

    [DllImport("__Internal")]
    private static extern void OnSetAudioPosition(float value);


    public static void EngineStarted()
    {
        Debug.Log("EngineStarted");
#if UNITY_WEBGL && !UNITY_EDITOR
        OnStartEngine();
#endif
    }
    
    public static void StartAudio()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        OnStartAudio();
#endif
    }

    public static void StopAudio()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        OnStopAudio();
#endif
    }

    public static void SetAudioPosition(float value)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        OnSetAudioPosition(value);
#endif
    }

    public static void MuteAudio()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        OnMute();
#endif
    }

    public static void UnmuteAudio()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        OnUnmute();
#endif
    }

    public static void Detect(int index)
    {
        OnExternalDetect?.Invoke(index);
    }

    public static void Compute(int index, Matrix4x4 matrix4X4)
    {
        OnExternalCompute?.Invoke(index, matrix4X4);
    }

    public static void Lost(int index)
    {
        OnExternalLost?.Invoke(index);
    }
}