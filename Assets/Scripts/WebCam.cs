using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class WebCam : MonoBehaviour
    {
        public Action OnInitialized { get; set; }

        private WebCamTexture webCamTexture;
        private IEnumerator initCoroutine;

        private int requestedWidth = 480;
        private int requestedHeight = 640;
        [SerializeField] private int requestedFPS = 30;
        
        private bool staticCameraIndex = false;
        [SerializeField] private int cameraIndex = 0;
        private int maxCameraIndex = 0;

        private void Awake()
        {
            OnInitialized += OnWebCamTextureToMatHelperInitialized;
        }

        private void Start()
        {
            requestedHeight = Screen.height;
            requestedWidth = Screen.width;
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

            cameraIndex = cameraIndex >= maxCameraIndex ? 0 : cameraIndex + 1;
            staticCameraIndex = true;
            Debug.Log($"Switch camera to {cameraIndex}");
            
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

            var devices = WebCamTexture.devices;
            maxCameraIndex = devices.Length - 1;
            var deviceName = string.Empty;

            if (staticCameraIndex)
            {
                deviceName = devices[cameraIndex].name;
            }
            else
            {
                Debug.Log("Webcam start found");

                int maxRes = 0, maxResCameraIndex = 0;
                for (var camIndex = 0; camIndex < devices.Length; camIndex++)
                {
                    var device = devices[camIndex];

                    Debug.Log($"Device {camIndex}\nname:{device.name}\nisFrontFacing: {device.isFrontFacing}" +
                              $"\nkind {device.kind}\ndepthCameraName {device.depthCameraName}");

                    if (device.availableResolutions == null)
                    {
                        Debug.Log($"Null resolutions in camera {camIndex}");
                        continue;
                    }
                    int camMaxRes = (device.availableResolutions.Max(r => r.height * r.width));

                    if (maxRes < camMaxRes)
                    {
                        maxRes = camMaxRes;
                        maxResCameraIndex = camIndex;
                    }
                }

                deviceName = devices[maxResCameraIndex].name;
                cameraIndex = maxResCameraIndex;
            }

            if (string.IsNullOrEmpty(deviceName)) {
                var wideCam = devices.FirstOrDefault(d => d.name.Contains("Задняя двойная широкоугольная камера"));
                if (string.IsNullOrEmpty(wideCam.name))
                    deviceName = devices[devices.Length > 1 ? 1 : 0].name;
                else
                    deviceName = wideCam.name;
                cameraIndex = devices.ToList().IndexOf(wideCam);
            }
            
            yield return StartCamera(deviceName);
        }

        private IEnumerator StartCamera(string deviceName)
        {
            webCamTexture = new WebCamTexture(deviceName, requestedWidth, requestedHeight, requestedFPS);
            webCamTexture.Play();
            yield return new WaitUntil(() => webCamTexture.didUpdateThisFrame);
            OnInitialized?.Invoke();
        }
    }
}