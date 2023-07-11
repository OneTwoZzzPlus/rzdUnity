using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.UI;
using Screen = Screens.Screen;

namespace OCRV.BigCalls.UI
{
    public class StartScreen : Screen
    {
        [SerializeField] private Button start;

        private void Awake()
        {
            start = start ?? GetComponent<Button>();
            Assert.IsNotNull(start);
        }

        public void Init(UnityAction OnStart)
        {
            start.onClick.AddListener(OnStart);
        }
    }
}