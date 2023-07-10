using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Screen = Screens.Screen;

namespace OCRV.BigCalls.UI
{
    public class MenuScreen : Screen
    {
        [SerializeField] private Button toAR;
        [SerializeField] private Button toRulebook;
        [SerializeField] private Button toDocs;

        public void Init(UnityAction showAR, UnityAction showRulebook, UnityAction showDocs)
        {
            toAR.onClick.AddListener(showAR);
            toRulebook.onClick.AddListener(showRulebook);
            toDocs.onClick.AddListener(showDocs);
        }
    }
}