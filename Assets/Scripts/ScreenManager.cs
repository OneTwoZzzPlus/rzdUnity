using System.Collections.Generic;
using System.Linq;
using OCRV.BigCalls.UI;
using UnityEngine;
using UnityEngine.Events;
using Screen = Screens.Screen;

public class ScreenManager : MonoBehaviour
{
    [SerializeField] private List<ScreenData> screens;

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        InitDefaultScreenFlow(screens);
        SwitchScreenTo(Screen.EType.Start);
    }

    
    
    private void InitDefaultScreenFlow(List<ScreenData> screens)
    {
        var titleScreen = (StartScreen) GetScreen(Screen.EType.Start);
        titleScreen.Init(() => SwitchScreenTo(Screen.EType.Menu));

        var menuScreen = (MenuScreen) GetScreen(Screen.EType.Menu);
        menuScreen.Init(
            GetSwitchToAction(Screen.EType.AR),
            GetSwitchToAction(Screen.EType.Rulebook),
            GetSwitchToAction(Screen.EType.Docs));

        var rulebookScreen = (RulebookScreen) GetScreen(Screen.EType.Rulebook);
        rulebookScreen.Init(GetSwitchToAction(Screen.EType.Menu));

        var arScreen = (ARScreen) GetScreen(Screen.EType.AR);
        arScreen.Init(
            GetSwitchToAction(Screen.EType.Menu),
            (index) =>
            {
                GetSwitchToAction(Screen.EType.Rulebook);
                rulebookScreen.ShowRule(index);

            });

        var docsScreen = (DocsScreen) GetScreen(Screen.EType.Docs);
        docsScreen.Init(GetSwitchToAction(Screen.EType.Menu));
    }

    public void SwitchScreenTo(Screen.EType screenType)
    {
        HideAllScreens();
        TurnScreen(GetScreen(screenType));
    }

    private void TurnScreen(Screen screen, bool active = true)
    {
        screen.gameObject.SetActive(active);
    }

    private UnityAction GetSwitchToAction(Screen.EType screenType)
    {
        return () => SwitchScreenTo(screenType);
    }

    private void HideAllScreens()
    {
        screens.ForEach(screenData => TurnScreen(screenData.ScreenObject, false));
    }

    private Screen GetScreen(Screen.EType screenType)
    {
        return screens.FirstOrDefault(s => s.ScreenType == screenType).ScreenObject;
    }
}