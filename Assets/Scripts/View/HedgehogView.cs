using DefaultNamespace;
using Main;
using UnityEngine;

namespace View
{
    public class HedgehogView : MonoBehaviour
    {
        [SerializeField] private GameObject arObjectPrefab;
        [SerializeField] private Transform trackableAnchor;
        
        private GameObject arObject;

        public void Show()
        {
            if (arObjectPrefab)
                arObject = Instantiate(arObjectPrefab, trackableAnchor);
        }
        
        public void Hide()
        {
            if (arObject)
                Destroy(arObject);
        }
        
        public void Move(Matrix4x4 matrix)
        {
            matrix = matrix.HouseholderReflection(Vector3.forward);
            matrix = matrix.MultiplyByNumber(.5f);

            trackableAnchor.SetTransformFromMatrixStab(matrix);
        }
    }
}