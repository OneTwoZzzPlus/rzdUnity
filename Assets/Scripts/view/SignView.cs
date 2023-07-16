using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View{
    public class SignView : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private GameObject foundFrame;
        [SerializeField] private Image signImage;
        [SerializeField] private TextMeshProUGUI foundText;

        public event Action buttonClicked;
        private bool isFound;

        private void Awake()
        {
            button.onClick.AddListener(()=> buttonClicked?.Invoke());
            foundFrame.gameObject.gameObject.SetActive(false);
        }

        public void SetSprite(Sprite sprite)
        {
            signImage.sprite = sprite;
        }

        public void SetFound(DateTime time)
        {
            foundFrame.gameObject.gameObject.SetActive(true);
            foundText.text = $"Обнаружен {time.Date.ToShortDateString()}";
        }
    }
}
