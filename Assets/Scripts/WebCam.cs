using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace
{
    public class WebCam : MonoBehaviour
    {
        public Action OnInitialized { get; set; }

        private WebCamTexture webCamTexture;
        private IEnumerator initCoroutine;

        // Cache devices
        private WebCamDevice[] devices;

        private int requestedWidth = 480;
        private int requestedHeight = 640;
        [SerializeField] private int requestedFPS = 30;
        
        private bool staticCameraIndex = false;
        [SerializeField] private int cameraIndex = 0;

        private void Awake()
        {
            OnInitialized += OnWebCamTextureToMatHelperInitialized;
        }

        private void Start()
        {
            requestedHeight = Display.displays[0].systemHeight;
            requestedWidth = Display.displays[0].systemWidth;
        }

        private void OnWebCamTextureToMatHelperInitialized()
        {
            if (!webCamTexture.isPlaying)
                return;
            var width = webCamTexture.width;
            var height = webCamTexture.height;

            Debug.Log($"Width: {width}\tHeight: {height}");


            var plane = gameObject.GetComponentInChildren<Renderer>();
            plane.material.mainTexture = webCamTexture;
            plane.transform.localScale = new Vector3(width, height, 1);

            var widthScale = (float)Screen.width / width;
            var heightScale = (float)Screen.height / height;
            if (widthScale > heightScale)
            {
                if (Camera.main is not null)
                    Camera.main.orthographicSize = (width * (float)Screen.height / Screen.width) / 2;
            }
            else
            {
                if (Camera.main != null)
                    Camera.main.orthographicSize = height / 2;
            }
        }

        private void OnDestroy()
        {
            if (webCamTexture)
                webCamTexture.Stop();
            OnInitialized -= OnWebCamTextureToMatHelperInitialized;
        }

        public void switchCamera()
        {
            if (webCamTexture)
                webCamTexture.Stop();

            cameraIndex = cameraIndex >= devices.Length-1 ? 0 : cameraIndex + 1;
            staticCameraIndex = true;

            Initiate();
        }

        public void Initiate()
        {
            if (initCoroutine is { })
            {
                StopCoroutine(initCoroutine);
                initCoroutine = null;
            }

            initCoroutine = InitiateCoroutine();
            StartCoroutine(initCoroutine);
        }

        private IEnumerator InitiateCoroutine()
        {
            yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
            if (!Application.HasUserAuthorization(UserAuthorization.WebCam))
            {
                yield break;
            }

            if (devices is null)
                devices = WebCamTexture.devices;

            if (devices.Length == 0)
                Debug.LogError("Missing cameras");

            var deviceName = string.Empty;

            if (staticCameraIndex)
            {
                Debug.Log($"Switch camera to {cameraIndex}");
                deviceName = devices[cameraIndex].name;
            }
            else
            {
                Debug.Log("Webcam start found");

                for (var camIndex = 0; camIndex < devices.Length; camIndex++)
                {
                    var device = devices[camIndex];

                    Debug.Log($"Device {camIndex}\nname:{device.name}\nisFrontFacing: {device.isFrontFacing}" +
                              $"\nkind: {device.kind}\ndepthCameraName: {device.depthCameraName}");

                    if (device.isFrontFacing)
                        continue;
                    Debug.Log($"Found camera {cameraIndex} in FOR");
                    deviceName = device.name;
                    cameraIndex = camIndex;
                    break;
                }
                if (string.IsNullOrEmpty(deviceName))
                {
                    var wideCam = devices.FirstOrDefault(d => d.name.Contains("адняя"));
                    if (!string.IsNullOrEmpty(wideCam.name))
                    {
                        cameraIndex = devices.ToList().IndexOf(wideCam);
                        deviceName = wideCam.name;
                        Debug.Log($"Found camera {cameraIndex} in RUS-BACK");
                    }

                    if (string.IsNullOrEmpty(wideCam.name)) { 
                        wideCam = devices.FirstOrDefault(d => d.name.Contains("ack"));
                        if (!string.IsNullOrEmpty(wideCam.name))
                        {
                            cameraIndex = devices.ToList().IndexOf(wideCam);
                            deviceName = wideCam.name;
                            Debug.Log($"Found camera {cameraIndex} in ENG-BACK");
                        }
                    }
                        
                    if (string.IsNullOrEmpty(wideCam.name))
                    {
                        cameraIndex = devices.Length > 1 ? 1 : 0;
                        deviceName = devices[cameraIndex].name;
                        Debug.Log($"Found camera {cameraIndex} in LAST");
                    }
                }
            }

            
            if (string.IsNullOrEmpty(deviceName))
            {
                Debug.LogWarning("Device not founded");
                deviceName = devices[cameraIndex].name;
                cameraIndex = 0;
            }
            yield return StartCamera(deviceName);
        }
            

        private IEnumerator StartCamera(string deviceName)
        {
            webCamTexture = new WebCamTexture(deviceName);
            webCamTexture.Play();
            yield return new WaitUntil(() => webCamTexture.didUpdateThisFrame);
            OnInitialized?.Invoke();
        }
    }
}