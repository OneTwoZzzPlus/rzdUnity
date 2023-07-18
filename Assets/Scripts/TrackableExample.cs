using Interfaces;
using Main;
using UnityEngine;

namespace DefaultNamespace
{
    public class TrackableExample : MonoBehaviour
    {
        [SerializeField] private GameObject arObjectPrefab;
        [SerializeField] private Transform trackableAnchor;
        [SerializeField] private int targetId = 32;

        private GameObject arObject;
        private ITargetTracker targetTracker;

        private void Start()
        {

            targetTracker = RootController.Instance.TargetTracker;

            targetTracker.TargetDetected += TargetDetectedHandler;
            targetTracker.TargetComputed += TargetComputedHandler;
            targetTracker.TargetLost += TargetLostHandler;
        }

        private void TargetDetectedHandler(int index)
        {
            if (index == targetId && arObjectPrefab)
                arObject = Instantiate(arObjectPrefab, trackableAnchor);
        }

        private void TargetComputedHandler(int index, Matrix4x4 matrix)
        {
            if (index != targetId)
                return;

            matrix = matrix.HouseholderReflection(Vector3.forward);
            matrix = matrix.MultiplyByNumber(.5f);

            trackableAnchor.SetTransformFromMatrixStab(matrix);
        }

        private void TargetLostHandler(int index)
        {
            if (index == targetId && arObject)
                Destroy(arObject);
        }

        private void OnDestroy()
        {
            targetTracker.TargetDetected -= TargetDetectedHandler;
            targetTracker.TargetComputed -= TargetComputedHandler;
            targetTracker.TargetLost -= TargetLostHandler;
        }
    }
}