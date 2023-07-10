using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Assertions;
using Screen = Screens.Screen;

namespace OCRV.BigCalls.UI
{
    public class RulebookScreen : Screen
    {
        [SerializeField] private Button toPrevious;

        private void Awake()
        {
            Assert.IsNotNull(toPrevious);
        }

        public void Init(UnityAction back)
        {
            toPrevious.onClick.AddListener(back);
        }

        public void ShowRule(int ruleId)
        {

        }
    }
}