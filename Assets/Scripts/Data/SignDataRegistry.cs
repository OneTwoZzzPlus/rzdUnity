using System.Collections.Generic;
using UnityEngine;
using Interfaces;
using System.Linq;

namespace Data
{
    [CreateAssetMenu(menuName = "Create SignDataRegistry", fileName = "SignDataRegistry", order = 0)]
    public class SignDataRegistry : ScriptableObject, IRegistry<SignData>
    {
        [SerializeField] private List<SignData> signsData = new List<SignData>();

        public SignData Get(int id)
        {
            var signData = signsData.FirstOrDefault(s => s.Id == id);
            if (signData == null) {
                Debug.LogWarning($"{id} sign is missing  in database");
            }
            return signData;
        }

        public IEnumerable<SignData> GetAll()
        {
            return signsData;
        }
    }
}
