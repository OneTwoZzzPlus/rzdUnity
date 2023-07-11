using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace SignsCV
{
    public class WebCam : MonoBehaviour
    {
        public Action OnInitialized { get; set; }

        private Texture2D texture;
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


            texture = new Texture2D(width, height, TextureFormat.RGBA32, false);
            var plane = gameObject.GetComponentInChildren<Renderer>();
            plane.material.mainTexture = texture;
            plane.transform.localScale = new Vector3(width, height, 1);

            var widthScale = (float)Screen.width / width;
            var heightScale = (float)Screen.height / height;
            if (widthScale < heightScale)
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

        private void Update()
        {
            if (!texture || !webCamTexture || !webCamTexture.isPlaying || !webCamTexture.didUpdateThisFrame)
                return;

            texture.SetPixels(webCamTexture.GetPixels());
            texture.Apply();
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

            if (staticCameraIndex)
            {
                webCamTexture = new WebCamTexture(devices[requestedCameraIndex].name, requestedWidth, requestedHeight, requestedFPS);
                webCamTexture.Play();
                yield return new WaitUntil(() => webCamTexture.didUpdateThisFrame);
                OnInitialized?.Invoke();
            }

            for (var cameraIndex = 0; cameraIndex < devices.Length; cameraIndex++)
            {
                var device = devices[cameraIndex];
                Debug.Log($"devices[{cameraIndex}].name: {device.name}");
                Debug.Log($"devices[{cameraIndex}].isFrontFacing: {device.isFrontFacing}");
                if (device.isFrontFacing)
                    continue;
                webCamTexture = new WebCamTexture(device.name, requestedWidth, requestedHeight, requestedFPS);
                webCamTexture.Play();
                yield return new WaitUntil(() => webCamTexture.didUpdateThisFrame);

                OnInitialized?.Invoke();
                break;
            }

            if (webCamTexture is not null)
                yield break;
            if (devices.Length <= 0)
                yield break;
            var deviceIndex = devices.Length > 1 ? 1 : 0;
            webCamTexture = new WebCamTexture(devices[deviceIndex].name, requestedWidth, requestedHeight, requestedFPS);
            webCamTexture.Play();
            yield return new WaitUntil(() => webCamTexture.didUpdateThisFrame);
            OnInitialized?.Invoke();
        }
    }
}