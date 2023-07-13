using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace View
{
    public class WindowController : MonoBehaviour, IWindowController
    {
        [SerializeField] private List<GameObject> windowPrefabs = new List<GameObject>();
        [SerializeField] private Transform canvas;

        private Dictionary<Type, IWindow> windows = new Dictionary<Type, IWindow>();

        private IWindow CreateWindow(Type windowType)
        {
            var first = windowPrefabs.FirstOrDefault(p => p.GetComponent(windowType) is {});
            if (first is null) {
                Debug.LogError($"Can't create window of type {windowType}");
                return null;
            }

            var window = Instantiate(first, canvas);

            var windowComponent = window.GetComponent<IWindow>();
            windows.Add(windowType, windowComponent);

            return windowComponent;

        }

        private IWindow GetOrCreateWindow(Type windowType)
        {
            var window = GetWindow(windowType);
            return window ?? CreateWindow(windowType);
        }

        private IWindow GetWindow (Type windowType)
        {
            if (windows.TryGetValue(windowType, out IWindow window)) {
                return window;
            }
            return null;
        }

        public void HideWindow(Type windowType) // Скрыть
        {
            var window = GetWindow(windowType);
            window?.Hide();
        }

        public IWindow ShowWindow(Type type) // Показать
        {
            var window = GetOrCreateWindow(type);
            if (window is null) return null;
            if (!window.IsShown) window.Show();
            return window;
        }

        
    }
}