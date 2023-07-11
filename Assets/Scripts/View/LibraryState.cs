using DefaultNamespace;
using Interfaces;

namespace View
{
    public class LibraryState : IState<ViewState>
    {
        public ViewState Id => ViewState.Library;

        public void Enter()
        {
            RootController.Instance.WindowController.ShowWindow(typeof(LibraryWindow));
        }

        public void Exit()
        {
            RootController.Instance.WindowController.HideWindow(typeof(LibraryWindow));
        }
    }
}