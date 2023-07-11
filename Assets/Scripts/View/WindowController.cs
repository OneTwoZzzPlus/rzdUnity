using Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WindowController : MonoBehaviour, IWindowController
{
    [SerializeField] List<GameObject> windowPrefabs = new List<GameObject>();
    [SerializeField] private Transform canvas;

    private Dictionary<Type, IWindow> windows = new Dictionary<Type, IWindow>();

    public IWindow ShowWindow(Type type)
    {
        var window =  GetOrCreate(type);
        if (window is null)
        {
            return null;
        }
        if (!window.IsShown) { window.Show(); }
        return window;
    }

    public void HideWindow(Type type)
    {
        var window = GetWindow(type);
        window?.Hide();

    }

    private IWindow CreateWindow (Type windowType)
    {
        var first = windowPrefabs.FirstOrDefault(p => p.GetComponent(windowType) is { });
        if (first is null)
        {
            Debug.LogError($"can't create window of type {windowType}");
            return null;
        }

        var window = Instantiate(first, canvas);


        return window.GetComponent<IWindow>();
    }

    private IWindow GetOrCreate(Type windowType)
    {
        var window = GetWindow(windowType);
        return window?? CreateWindow(windowType);
    }

    private IWindow GetWindow(Type windowType)
    {
        if(windows.TryGetValue(windowType, out var window))
        {
            return window;
        }
        return null;
    }

}
