
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using View;

namespace View
{
    public class LibraryWindow : BaseWindow
    {
        public event Action ARButtonClicked;

        public event Action<int> SignButtonClicked;

        [SerializeField] private Button ARButton;
        [SerializeField] private Button rewindButton;
        [SerializeField] private ScrollRect scroll;
        [SerializeField] private SignView verticalSignPrefab;
        [SerializeField] private SignView horizontalSignPrefab;
        [SerializeField] private GameObject libraryPanelPrefab;
        [SerializeField] private Transform contentParent;

        [SerializeField] private float scrollStartSpeed;
        [SerializeField] private float scrollDuration;

        private Dictionary<int, SignView> signViews = new Dictionary<int, SignView>();
        private Coroutine scrollCoroutine;


        private void Awake()
        {
            ARButton.onClick.AddListener(() => ARButtonClicked?.Invoke());
            scroll.onValueChanged.AddListener(v => rewindButton.gameObject.SetActive(v.y < 0.8f));
            rewindButton.onClick.AddListener(ScrollUp);
            rewindButton.gameObject.SetActive(false);
        }

        void ScrollUp()
        {
            if (scrollCoroutine is not null)
            {
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



        public void SetSignFound(int id, bool isFound, DateTime time) 
        { 
            if(isFound && signViews.TryGetValue(id, out var view))
            {
                view.SetFound(time);
            }
        }

        public void CreateSign(int id, Sprite sprite)
        {
            if (signViews.ContainsKey(id))
                return;

            var prefab = IsVertical(sprite) ? verticalSignPrefab : horizontalSignPrefab;
            var panel = GetOrCreatePanel(sprite);
            
            if (panel is null) 
            {
                Debug.LogError("Sign object isn't created");
                return;
            }

            var sign = Instantiate(prefab, panel.transform);
            if ( sign is null)
            {
                Debug.LogError("Sign obj isn't created");
                return;
            }

            if (signViews.ContainsKey(id))
            {
                Debug.LogError($"Dictionary already contains sign with id {id}");
            }
            else
                signViews.Add(id, sign);

            sign.SetSprite(sprite);
            sign.buttonClicked += SignButtonClickedHandler(id);
            
        }

        private Action SignButtonClickedHandler(int id)
        {
            return () => SignButtonClicked?.Invoke(id);
        }

        private GameObject GetOrCreatePanel(Sprite nextSprite)
        {
            if (IsVertical(nextSprite))
            {
                for (var i = contentParent.transform.childCount; i > 0; i--)
                {
                    var lastPanel = contentParent.transform.GetChild(i - 1);
                    if (lastPanel.childCount > 1)
                        break;
                    if (lastPanel && HaveSpace(lastPanel))
                        return lastPanel.gameObject;
                }
            }

            var panel = Instantiate(libraryPanelPrefab, contentParent);
            if (panel is null)
            {
                Debug.LogError("Panel obj isn't created");
                return null;
            }
            return panel.gameObject;
        }

        private bool HaveSpace(Transform panel)
        {
            if (panel.childCount == 0)
                return true;
            if (panel.childCount > 1)
                return false;
            var child = panel.GetChild(0);
            if (child)
            {
                var sprite = child.GetComponent<Image>();
                if (sprite)
                    return IsVertical(sprite.sprite);
            }
            return false;
        }

        private bool IsVertical(Sprite sprite)
        {
            return sprite.texture.height > sprite.texture.width;
        }
    }

}
