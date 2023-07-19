using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace View
{
    public class SignView : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private GameObject foundFrame;
        [SerializeField] private TextMeshProUGUI foundText;
        [SerializeField] private Image signImage;

        public event Action ButtonClicked;

        private bool isFound;

        private void Awake()
        {
            button.onClick.AddListener(() => ButtonClicked?.Invoke());
            foundFrame.SetActive(isFound);
        }

        public void SetSprite(Sprite sprite)
        {
            signImage.sprite = sprite;
        }

        public void SetText(string text)
        {
            foundText.text = text;
        }

        public void SetFound(DateTime time)
        {
            isFound = true;
            foundFrame.SetActive(true);
            foundText.text = $"Обнаружен {time.Date.ToShortDateString()}";
        }

        public bool Interactable { get => button.interactable; 
            set => button.interactable = value; } 
        
    }
}