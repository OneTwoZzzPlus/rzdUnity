using DefaultNamespace;
using Interfaces;

namespace View
{
    public class InfoState : IState<ViewState>
    {
        public ViewState Id => ViewState.Info;

        public void Enter()
        {
            RootController.Instance.WindowController.ShowWindow(typeof(SignInfoWindow));
        }

        public void Exit()
        {
            RootController.Instance.WindowController.HideWindow(typeof(SignInfoWindow));
        }
    }
}