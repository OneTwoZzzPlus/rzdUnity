using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.UI;
using Screen = Screens.Screen;

namespace OCRV.BigCalls.UI
{
    public class ARScreen : Screen
    {
        [SerializeField] private Button toMenu;
        [SerializeField] private Button toRule;

        public Action OnDetect;

        private int ruleIndex;

        private void Awake()
        {
            Assert.IsNotNull(toMenu);
            Assert.IsNotNull(toRule);
        }

        public void Init(UnityAction showMenu, UnityAction<int> showRule)
        {
            toMenu.onClick.AddListener(showMenu);
            toRule.onClick.AddListener(() => showRule(ruleIndex));
        }

        public bool Scan()
        {
            var isDetected = false;

            // pseudo code
            // isDetected = ARMind.Detect();
            // set rule index by detected icon

            return isDetected;
        }

        private void Update()
        {
            if (Scan())
                OnDetect?.Invoke();
        }
    }
}