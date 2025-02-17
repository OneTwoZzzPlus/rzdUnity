using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using View;

public class LibraryWindow : BaseWindow
{
    public event Action ARButtonClicked;
    public event Action<int> SignButtonClicked;

    [SerializeField] private Button ARButton;
    [SerializeField] private Button rewindButton;
    [SerializeField] private ScrollRect scroll;
    [SerializeField] private float scrollDuration = 3f;
    [SerializeField] private SignView verticalSignPrefab;
    [SerializeField] private SignView horizontalSignPrefab;
    [SerializeField] private GameObject libraryPanelPrefab;
    [SerializeField] private Transform contentParent;

    [SerializeField] private Sprite secretSprite;
    

    private Dictionary<int, SignView> signViews = new();
    private Coroutine scrollCoroutine;

    private void Awake()
    {
        ARButton.onClick.AddListener(() => ARButtonClicked?.Invoke());
        scroll.onValueChanged.AddListener((v) => rewindButton.gameObject.SetActive(v.y < 0.8f));
        rewindButton.onClick.AddListener(() => ScrollUp());
        rewindButton.gameObject.SetActive(false);
    }

    private void ScrollUp()
    {
        if (scrollCoroutine is not null) { 
            StopCoroutine(scrollCoroutine);
            scrollCoroutine = null;
        }
        
        scrollCoroutine = StartCoroutine(ScrollUp(scrollDuration));
    }

    IEnumerator ScrollUp(float duration)
    {
        var time = duration;
        var ratio = 1f;
        while (time > 0f)
        {
            time -= Time.deltaTime;
            ratio = 1 - time / duration;
            scroll.verticalNormalizedPosition = Mathf.Lerp(scroll.verticalNormalizedPosition, 1f, ratio * ratio);
            yield return null;
        }
    }

    public void SetSignFound(int id, int unlockCount, bool isFound, DateTime time)
    {
        if (signViews.TryGetValue(id, out var view))
        {
            if (isFound)
                view.SetFound(time);
            else
                view.SetUnlockProgress(unlockCount);
        }
    }

    public void UpdateSign(int id, Sprite sprite, bool isLocked)
    {
        SignView sign;
        if (!signViews.TryGetValue(id, out sign)) {
            sign = CreateSign(id, sprite);
        }
        sign.SetSprite(isLocked ? secretSprite : sprite);
        sign.Interactable = !isLocked;
    }

    private SignView CreateSign(int id, Sprite sprite)
    {
        var prefab = IsVertical(sprite) ? verticalSignPrefab : horizontalSignPrefab;
        var panel = GetOrCreatePanel(sprite);

        if (panel is null)
        {
            Debug.LogError("Panel object isn't create");
            return null;
        }

        var sign = Instantiate(prefab, panel);

        if (sign is null)
        {
            Debug.LogError("Sign object isn't create");
            return null;
        }

        if (signViews.ContainsKey(id))
            Debug.LogError($"Dictionary alredy contains sign with id {id}");
        else
            signViews.Add(id, sign);

        sign.ButtonClicked += SignButtonClickHandler(id);
        return sign;
    }

    private Action SignButtonClickHandler(int id) => () => SignButtonClicked?.Invoke(id);

    private Transform GetOrCreatePanel(Sprite nextSprite)
    {
        if (IsVertical(nextSprite)) {
            for (var i = contentParent.transform.childCount; i > 0; i--)
            {
                var lastPanel = contentParent.transform.GetChild(i - 1);
                if (lastPanel.childCount > 1) break;
                if (lastPanel && HaveSpace(lastPanel)) return lastPanel;
            }
        }

        var panel = Instantiate(libraryPanelPrefab, contentParent);

        return panel.transform;
    }

    private bool HaveSpace(Transform panel)
    {
        if (panel.childCount == 0) return true;
        if (panel.childCount > 1) return false;
        var child = panel.GetChild(0);
        if (child)
        {
            var image = child.GetComponent<Image>();
            if (image) return IsVertical(image.sprite);
        }
        return false;
    }

    private bool IsVertical(Sprite sprite)
    {
        return sprite.rect.height > sprite.rect.width;
    }

}
