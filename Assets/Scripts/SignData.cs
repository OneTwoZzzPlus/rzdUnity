using Interfaces;
using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(menuName = "Create SignData", fileName = "SignData", order = 0)]
    public class SignData : ScriptableObject, IModel
    {
        public int Id => index;

        [SerializeField] private int index;
        [SerializeField] private Sprite sprite;
        [SerializeField] private string description;
    }
}