using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CamTest : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private Button back;
    [SerializeField] private Button next;

    private WebCamTexture webcamTexture;
    private int deviceIndex;
    private void Awake()
    {
        back.onClick.AddListener(() => {
            deviceIndex = deviceIndex > 0 ? deviceIndex - 1 : 0;
            Play(deviceIndex);
        });
        next.onClick.AddListener(() => {
            deviceIndex = deviceIndex < WebCamTexture.devices.Length - 1 ? deviceIndex + 1 : 0;
            Play(deviceIndex);
        });
    }

    void Start()
    {
        Play(deviceIndex);
    }

    private void Play(int device)
    {
        if (WebCamTexture.devices.Length > device) {
            if (webcamTexture is not null && webcamTexture.isPlaying) {
                webcamTexture.Stop();
                Destroy(webcamTexture);
            }
            var deviceName = WebCamTexture.devices[device].name;
            text.text = deviceName;
            webcamTexture = new WebCamTexture(deviceName);
            RawImage rawImage = GetComponent<RawImage>();
            rawImage.texture = webcamTexture;
            webcamTexture.Play();
        }
    }
}
