using Interfaces;
using UnityEngine;

namespace View
{
    public abstract class BaseWindow : MonoBehaviour, IWindow
    {
        public bool IsShown => gameObject.activeSelf;
        public virtual void Hide() => gameObject.SetActive(false);
        public virtual void Show() => gameObject.SetActive(true);
    }
}