using Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWindow : MonoBehaviour, IWindow
{
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

    public bool IsShown => gameObject.activeSelf;
}
