using Model;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using View;

public class SignInfoWindow : BaseWindow
{
    public event Action BackButtonClicked;

    [SerializeField] private Button backButton;
    [SerializeField] private TextMeshProUGUI signNumberText;
    [SerializeField] private TextMeshProUGUI signNameText;
    [SerializeField] private TextMeshProUGUI signDescriptionText;
    [SerializeField] private TextMeshProUGUI foundDateText;
    [SerializeField] private Image signImage;

    private void Awake()
    {
        backButton.onClick.AddListener(() => BackButtonClicked?.Invoke());
    }

    public void SetImage(Sprite image)
    {
        signImage.sprite = image;
    }

    public void SetSignNumber(string signNumber)
    {
        signNumberText.text = $"�{signNumber}";
    }
    public void SetSignName(string signName)
    {
        signNameText.text = signName;
    }
    public void SetSignDescription(string signDescription)
    {
        signDescriptionText.text = signDescription;
    }

    public void SetFound(bool isFound, DateTime foundTime)
    {
        foundDateText.gameObject.SetActive(isFound);
        if (isFound)
            foundDateText.text = $"���������<br>{foundTime.ToShortDateString()}";
    }
}
