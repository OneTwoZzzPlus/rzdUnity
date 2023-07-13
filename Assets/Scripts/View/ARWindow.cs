using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using View;

public class ARWindow : BaseWindow
{
    public event Action LibraryButtonClicked;
    public event Action SignButtonClicked;

    [SerializeField] private Button libraryButton;
    [SerializeField] private Button signButton;
    [SerializeField] private TextMeshProUGUI signNumberText;
    [SerializeField] private TextMeshProUGUI signNameText;
    [SerializeField] private float delay = 2f;
    private bool lostFlag;

    private Coroutine courotineWLFD;

    private void Awake()
    {
        libraryButton.onClick.AddListener(() => LibraryButtonClicked?.Invoke());
        signButton.onClick.AddListener(() => SignButtonClicked?.Invoke());
    }

    private void Start()
    {
        signButton.gameObject.SetActive(false);
    }

    public override void Show()
    {
        signButton.gameObject.SetActive(false);
        base.Show();
    }

    public override void Hide()
    {
        StopAllCoroutines();
        signButton.gameObject.SetActive(false);
        base.Show();
    }

    public void SetSignNumber(string signNumber)
    {
        signNumberText.text = $"Чэръ Й{signNumber}";
    }
    public void SetSignName(string signName)
    {
        signNameText.text = signName;
    }
    public void ShowSignButton()
    {
        lostFlag = false;
        signButton.gameObject.SetActive(true);
    }
    public void HideSignButton()
    {
        if (courotineWLFD is not null)
        {
            StopCoroutine(courotineWLFD);
            courotineWLFD = null;
        }
        lostFlag = true;
        courotineWLFD = StartCoroutine(WaitLostForDuration());
    }

    IEnumerator WaitLostForDuration()
    {
        yield return new WaitForSeconds(delay);
        if (lostFlag) signButton.gameObject.SetActive(false);
    }
}
