using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;

public class TargetTest : MonoBehaviour
{
    [SerializeField] private WebCam webCam;
    [SerializeField] private RealTargetTracker targetTracker;
    [SerializeField] private TMP_Text text;
    private void Awake()
    {
        webCam.OnInitialized += OnWebcamInitialized;
        webCam.Initiate();
        
        targetTracker.TargetDetected += TargetDetectedHandler;
        targetTracker.TargetLost += TargetLostHandler;
    }

    private void TargetLostHandler(int obj)
    {
        text.text = $"TargetLost {obj}";
    }

    private void TargetDetectedHandler(int obj)
    {
        text.text = $"TargetDetected {obj}";
    }

    private void OnWebcamInitialized()
    {
        WebGLBridge.EngineStarted();
    }
}
