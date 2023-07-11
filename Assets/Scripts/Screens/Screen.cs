using System;
using UnityEngine;

namespace Screens
{
    public abstract class Screen : MonoBehaviour
    {
        public enum EType { Start, Menu, AR, Rulebook, Docs }

        public EType Type;

        public Action OnShow;
        public Action OnHide;

        private void OnEnable()
        {
            OnShow?.Invoke();
        }

        private void OnDisable()
        {
            OnHide?.Invoke();
        }
    }
}