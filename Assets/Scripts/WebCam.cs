using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

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
        [SerializeField] private bool staticCameraIndex = false;
       
        [SerializeField][Range(0, 6)] private int requestedCameraIndex = 0;
        private void Start()
        {
            requestedHeight = Screen.height;
            requestedWidth = Screen.width;
        }

        public void Initiate()
        {
            if (initCoroutine is { })
            {
                StopCoroutine(initCoroutine);
                initCoroutine = null;
            }

            OnInitialized += OnWebCamTextureToMatHelperInitialized;
            initCoroutine = InitiateCoroutine();
            StartCoroutine(initCoroutine);
        }

        private void OnWebCamTextureToMatHelperInitialized()
        {
            if (!webCamTexture.isPlaying)
                return;
            var width = webCamTexture.width;
            var height = webCamTexture.height;

            Debug.Log($"Width: {width}");
            Debug.Log($"Height: {height}");


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
        }

        private IEnumerator InitiateCoroutine()
        {
            yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
            if (!Application.HasUserAuthorization(UserAuthorization.WebCam))
            {
                yield break;
            }

            var devices = WebCamTexture.devices;
            Debug.Log("webcam start found");
            var deviceName = string.Empty;
            
            if (staticCameraIndex) {
                deviceName = devices[requestedCameraIndex].name;
            }

            for (var cameraIndex = 0; cameraIndex < devices.Length; cameraIndex++)
            {
                var device = devices[cameraIndex];
                Debug.Log($"devices[{cameraIndex}].name: {device.name}");
                Debug.Log($"devices[{cameraIndex}].isFrontFacing: {device.isFrontFacing}");
                Debug.Log($"devices[{cameraIndex}].kind {device.kind}" );
                Debug.Log($"devices[{cameraIndex}].depthCameraName {device.depthCameraName}" );
                
                if (device.isFrontFacing)
                    continue;
                deviceName = device.name;
                break;
            }

            if (string.IsNullOrEmpty(deviceName)) {
                var wideCam = devices.FirstOrDefault(d => d.name.Contains("Задняя двойная широкоугольная камера")).name;
                if (string.IsNullOrEmpty(wideCam))
                    deviceName = devices[devices.Length > 1 ? 1 : 0].name;
                else
                    deviceName = wideCam;
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