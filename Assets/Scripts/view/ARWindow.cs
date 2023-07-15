using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class ARWindow : BaseWindow
    {
        public event Action SignButtonClicked;
        public event Action LibraryButtonClicked;

        [SerializeField] private Button signButton;
        [SerializeField] private Button libraryButton;
        [SerializeField] private TMP_Text signTitleText;
        [SerializeField] private TMP_Text signNameText;

        private Coroutine waitCoroutine;

        private void Awake()
        {
            signButton.onClick.AddListener(() => SignButtonClicked?.Invoke());
            libraryButton.onClick.AddListener(() => LibraryButtonClicked?.Invoke());
            signButton.gameObject.SetActive(false);
        }


        public override void Hide()
        {
            StopAllCoroutines();
            signButton.gameObject.SetActive(false);
            base.Hide();
        }

        public void SetSignNumber(string signNumber)
        {
            signTitleText.text = $"Чэръ Й{signNumber}";
        }

        public void SetSignName(string signName)
        {
            signNameText.text = signName;
        }

        public void ShowSignButton()
        {
            signButton.gameObject.SetActive(true);
        }

        public void HideSignButton()
        {
            if (waitCoroutine is { })
            {
                StopCoroutine(waitCoroutine);
                waitCoroutine = null;
            }
            waitCoroutine = StartCoroutine(
                Wait(3f, () => signButton.gameObject.SetActive(false)));
        }

        private IEnumerator Wait(float duration, Action doAction)
        {
            yield return new WaitForSeconds(duration);
            doAction?.Invoke();
        }

    }
}
