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
    public event Action CamSwitchButtonClicked;

    [SerializeField] private Button libraryButton;
    [SerializeField] private Button signButton;
    [SerializeField] private Button camSwitchButton;
    [SerializeField] private TextMeshProUGUI signNumberText;
    [SerializeField] private TextMeshProUGUI signNameText;
    [SerializeField] private float delay = 2f;
    private bool lostFlag;

    private Coroutine courotineWLFD;

    private void Awake()
    {
        libraryButton.onClick.AddListener(() => LibraryButtonClicked?.Invoke());
        signButton.onClick.AddListener(() => SignButtonClicked?.Invoke());
        camSwitchButton.onClick.AddListener(() => CamSwitchButtonClicked?.Invoke());
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

    public void SetActiveCamSwitchButton(bool isActive)
    {
        camSwitchButton.interactable = isActive;
    }

    public void SetSignNumber(string signNumber)
    {
        signNumberText.text = $"���� �{signNumber}";
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
        courotineWLFD = StartCoroutine(WaitLostForDuration(
            () => { if (lostFlag) signButton.gameObject.SetActive(false); }));
    }

    IEnumerator WaitLostForDuration(Action doAction)
    {
        yield return new WaitForSeconds(delay);
        doAction?.Invoke();
    }
}
