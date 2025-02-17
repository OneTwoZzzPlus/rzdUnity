using DefaultNamespace;
using Main;
using UnityEngine;

namespace View
{
    public class ArView : MonoBehaviour
    {
        [SerializeField] private Transform trackableAnchor;
        
        private GameObject arObject;

        public void Show(GameObject arObjectPrefab)
        {
            if (arObjectPrefab is null)
                return;
            arObject = Instantiate(arObjectPrefab, trackableAnchor);
        }
        
        public void Hide()
        {
            if (arObject is null)
                return;
            Destroy(arObject);
            arObject = null;

        }
        
        public void Move(Matrix4x4 matrix)
        {
            matrix = matrix.HouseholderReflection(Vector3.forward);
            matrix = matrix.MultiplyByNumber(.5f);

            trackableAnchor.SetTransformFromMatrixStab(matrix);
        }
    }
}