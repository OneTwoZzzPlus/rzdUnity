using Interfaces;
using Main;
using UnityEngine;

namespace DefaultNamespace
{
    public class TrackableExample : MonoBehaviour
    {
        [SerializeField] private WebCam webCam;
        [SerializeField] private GameObject arObjectPrefab;
        [SerializeField] private Transform trackableAnchor;

        private GameObject arObject;
        private ITargetTracker targetTracker;

        private void Start()
        {

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
            Debug.Log("Detected " + index);
            if (arObjectPrefab)
                arObject = Instantiate(arObjectPrefab, trackableAnchor);
        }

        private void TargetComputedHandler(int index, Matrix4x4 matrix)
        {
            matrix = matrix.HouseholderReflection(Vector3.forward);
            matrix = matrix.MultiplyByNumber(.5f);

            trackableAnchor.SetTransformFromMatrixStab(matrix);
        }

        private void TargetLostHandler(int index)
        {
            Debug.Log("Lost " + index);
            if (arObject)
                Destroy(arObject);
        }

        private void OnDestroy()
        {
            WebGLBridge.OnExternalDetect -= TargetDetectedHandler;
            WebGLBridge.OnExternalCompute -= TargetComputedHandler;
            WebGLBridge.OnExternalLost -= TargetLostHandler;
        }
    }
}