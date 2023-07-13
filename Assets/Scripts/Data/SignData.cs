using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "Create SignData", fileName = "SignData", order = 0)]

    public class SignData : ScriptableObject
    {
        public int Id;
        public Sprite Sprite;
        public string Name;
        public string Number;
        [Multiline(10)] public string Description;

    }
}