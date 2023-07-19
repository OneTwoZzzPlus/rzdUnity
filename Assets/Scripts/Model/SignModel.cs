using Data;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Model
{
    [Serializable]
    public class SignModel: IModel
    {
        [SerializeField]
        private bool isFound;

        public bool IsFound {
            get => isFound;
            set {
                isFound = value;
            }
        }

        [SerializeField]
        private bool isLocked;
        
        public bool IsLocked {
            get => isLocked;
        }

        public void UpdateLockedStatus(IEnumerable<int> foundIds)
        {
            isLocked = signData.GetLocked(foundIds);
        }
        
        public GameObject ArObject => signData.ArObject;
        
        [SerializeField] private long foundTime;
        public DateTime FoundTime
        {
            get => DateTime.FromFileTimeUtc(foundTime);
            set => foundTime = value.ToFileTimeUtc();
        }        

        [NonSerialized]
        private readonly SignData signData;
        public int Id => signData.Id;
        public Sprite Sprite => signData.Sprite;
        public string Name => signData.Name;
        public string Number => signData.Number;
        public string Description => signData.Description;
        
        public void Save()
        {
            var saveString = JsonUtility.ToJson(this);
            PlayerPrefs.SetString(Id.ToString(), saveString);
        }

        public void Load() 
        {
            var loadString = PlayerPrefs.GetString(Id.ToString());
            if (!string.IsNullOrEmpty(loadString))
            {
                var model = JsonUtility.FromJson<SignModel>(loadString);
                IsFound = model.IsFound;
                FoundTime = model.FoundTime;
                isLocked = model.isLocked;
            }
        }

#if UNITY_EDITOR
        [MenuItem("PlayerPrefs/Reset")]
        public static void Reset()
        {
            PlayerPrefs.DeleteAll();
        }
#endif

        public SignModel(SignData signData)
        {
            this.signData = signData;
        }

        public class Factory : IFactory<SignData, SignModel>
        {
            public SignModel Create(SignData settings)
            {
                return new SignModel(settings);
            }
        }

    }
}