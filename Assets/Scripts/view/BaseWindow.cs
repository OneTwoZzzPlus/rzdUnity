using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;

namespace View
{ 
    public abstract class BaseWindow : MonoBehaviour, IWindow
    {

        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        // Update is called once per frame
        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }

        public bool IsShown => gameObject.activeSelf;
    }
}
