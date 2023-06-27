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

        private void Start()
        {
            if (webCam)
            {
                webCam.OnInitialized += OnWebcamInitialized;
                webCam.Initiate();
            }

            WebGLBridge.OnExternalDetect += OnExternalDetect;
            WebGLBridge.OnExternalCompute += OnExternalCompute;
            WebGLBridge.OnExternalLost += OnExternalLost;
        }

        private void OnWebcamInitialized()
        {
            WebGLBridge.EngineStarted();
        }

        private void OnExternalDetect(int index)
        {
            Debug.Log("Detected " + index);
            if (arObjectPrefab)
                arObject = Instantiate(arObjectPrefab, trackableAnchor);
        }

        private void OnExternalCompute(int index, Matrix4x4 matrix)
        {
            matrix = matrix.HouseholderReflection(Vector3.forward);
            matrix = matrix.MultiplyByNumber(.5f);

            trackableAnchor.SetTransformFromMatrixStab(matrix);
        }

        private void OnExternalLost(int index)
        {
            Debug.Log("Lost " + index);
            if (arObject)
                Destroy(arObject);
        }

        private void OnDestroy()
        {
            WebGLBridge.OnExternalDetect -= OnExternalDetect;
            WebGLBridge.OnExternalCompute -= OnExternalCompute;
            WebGLBridge.OnExternalLost -= OnExternalLost;
        }
    }
}