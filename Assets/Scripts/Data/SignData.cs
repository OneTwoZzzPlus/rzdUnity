using System.Collections.Generic;
using System.Linq;
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

        public List<SignData> SignsForUnlock;
        public GameObject ArObject;
        
        public bool GetLocked(IEnumerable<int> foundIds)
        {
            if (SignsForUnlock.Count == 0)
                return false;
            var unlockIds = SignsForUnlock.Select(s => s.Id);
            
            
            return !unlockIds.All(i => foundIds.Contains(i));
        }
    }
}