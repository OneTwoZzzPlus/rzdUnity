using Data;
using UnityEngine;
using Data;
using Interfaces;
using UnityEngine;
using System;

namespace Model{
    public class SignModel : IModel
    {
        public int Id => signData.Id;
        public Sprite Sprite => signData.Sprite;
        public string Name => signData.Name;
        public string Number => signData.Number;
        public string Description => signData.Description;

        public bool IsFound;
        public DateTime FoundTime;

        private SignData signData;


        public SignModel(SignData signData)
        {
            this.signData = signData;
        }

        public void Save()
        {

        }

        public void Load()
        {

        }

        public class Factory: IFactory<SignData, SignModel>
        {
            public SignModel Create(SignData settings) 
            {
                return new SignModel(settings);
            }
        }
    }
}