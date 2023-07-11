using Interfaces;
using System.Collections;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class OpenButton : MonoBehaviour
    {
        private ITargetTracker targetTracker;

        [SerializeField] private WebCam webCam;
        [SerializeField] private GameObject button;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private float delay;
        private bool lostFlag;

        private void Start()
        {
            button.SetActive(false);

            targetTracker = RootController.Instance.TargetTracker;

            if (webCam)
            {
                webCam.OnInitialized += OnWebcamInitialized;
                webCam.Initiate();
            }

            targetTracker.TargetDetected += TargetDetectedHandler;
            targetTracker.TargetComputed += TargetComputedHandler;
            targetTracker.TargetLost += TargetLostHandler;
        }

        private void OnWebcamInitialized()
        {
            WebGLBridge.EngineStarted();
        }

        private void TargetDetectedHandler(int index)
        {
            lostFlag = false;
            button.SetActive(true);
            text.text = $"Detected {index}";
        }

        private void TargetLostHandler(int index)
        {
            lostFlag = true;
            text.text = $"Lost {index}";
            StartCoroutine(LostAsWait());
        }

        IEnumerator LostAsWait()
        {
            yield return new WaitForSeconds(delay);
            if (lostFlag) button.SetActive(false);
        }

        private void TargetComputedHandler(int index, Matrix4x4 matrix) { }

        private void OnDestroy()
        {
            WebGLBridge.OnExternalDetect -= TargetDetectedHandler;
            WebGLBridge.OnExternalCompute -= TargetComputedHandler;
            WebGLBridge.OnExternalLost -= TargetLostHandler;
        }
    }
}