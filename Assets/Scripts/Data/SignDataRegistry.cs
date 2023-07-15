using Interfaces;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName ="Create SignDataRegistry", fileName = "SignDataRegistry", order = 0)]
    public class SignDataRegistry : ScriptableObject, IRegistry<SignData>
    {
        [SerializeField]
        private List<SignData> signData = new List<SignData>();
        public SignData Get(int id)
        {
           return signData.FirstOrDefault(s => s.Id == id);
        }

        public IEnumerable<SignData> GetAll()
        {
            return signData;
        }
    }
}