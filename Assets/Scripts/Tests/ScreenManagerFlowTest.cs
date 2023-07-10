using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Screen = Screens.Screen;

namespace OCRV.BigCalls.UI.Tests
{
    public class ScreenManagerFlowTest : MonoBehaviour
    {

        [SerializeField][Range(1, 10)] private float delay = 2f;

        private ScreenManager screenManager;

        private void Start()
        {
            screenManager = FindAnyObjectByType<ScreenManager>();

            Assert.IsNotNull(screenManager, "Screen manager not found!");

            screenManager.Init();

            DetectCaseFlowTest();
            NoDetectCaseFlowTest();
        }

        public void DetectCaseFlowTest()
        {
            Debug.Log("[ScreenManagerFlowTest][DetectCaseFlowTest]");
            var screensToIterate = new List<Screen.EType> {
                Screen.EType.Start,
                Screen.EType.Menu,
                Screen.EType.AR,
                Screen.EType.Rulebook,
                Screen.EType.Menu
            };

            StartCoroutine(IterateScreens(screensToIterate));
        }

        public void NoDetectCaseFlowTest()
        {
            Debug.Log("[ScreenManagerFlowTest][NoDetectCaseFlowTest]");

            var screensToIterate = new List<Screen.EType> {
                Screen.EType.Start,
                Screen.EType.Menu,
                Screen.EType.AR,
                Screen.EType.Menu
            };

            StartCoroutine(IterateScreens(screensToIterate));
        }

        private IEnumerator IterateScreens(List<Screen.EType> screens)
        {
            foreach (var screen in screens)
            {
                screenManager.SwitchScreenTo(screen);
                yield return new WaitForSeconds(delay);
                Debug.Log($"[ScreenManagerFlowTest][NoDetectCaseFlowTest][IterateScreens] Switched to: {screen}");
            }
        }
    }
}