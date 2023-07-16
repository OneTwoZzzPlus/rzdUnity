using Data;
using Interfaces;
using System;
using System.Data;
using UnityEngine;

namespace Model
{
    public class SignModel: IModel
    {
        private SignData signData;
        public int Id => signData.Id;
        public Sprite Sprite => signData.Sprite;
        public string Name => signData.Name;
        public string Number => signData.Number;
        public string Description => signData.Description;

        public bool IsFound;
        public DateTime FoundTime;

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