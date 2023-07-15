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

    public override void Hide()
    {
        StopAllCoroutines();
        signButton.gameObject.SetActive(false);
        base.Hide();
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
        courotineWLFD = StartCoroutine(WaitLostForDuration(1f, 
            () => { if (lostFlag) signButton.gameObject.SetActive(false); }));
    }

    IEnumerator WaitLostForDuration(float delay, Action doAction)
    {
        yield return new WaitForSeconds(delay);
        doAction?.Invoke();
    }
}
