using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.UI;
using Screen = Screens.Screen;

namespace OCRV.BigCalls.UI
{
    public class DocsScreen : Screen
    {
        [SerializeField] private Button toMenu;

        private void Awake()
        {
            Assert.IsNotNull(toMenu);
        }

        public void Init(UnityAction showMenu)
        {
            toMenu.onClick.AddListener(showMenu);
        }

        public void ShowDoc(int index)
        {

        }
    }
}