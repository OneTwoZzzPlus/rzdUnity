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
            if (signImage.sprite != sprite)
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
            foundText.text = $"��������� {time.Date.ToShortDateString()}";
        }

        public void SetUnlockProgress(int count)
        {
            if (count <= 0)
            {
                isFound = false;
                foundFrame.SetActive(false);
            }
            else
            {
                isFound = true;
                foundFrame.SetActive(true);
                foundText.text = $"�������� {count} ������";
            }
        }

        public bool Interactable { get => button.interactable; 
            set => button.interactable = value; } 
        
    }
}