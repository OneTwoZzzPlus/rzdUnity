using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using View;

namespace View
{
    public class LibraryWindow : BaseWindow
    {
        public event Action ARButtonClicked;

        [SerializeField] private Button ARButton;

        private void Awake()
        {
            ARButton.onClick.AddListener(() => ARButtonClicked?.Invoke());
        }
    }
}
