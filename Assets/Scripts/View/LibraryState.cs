using DefaultNamespace;
using Interfaces;
using Model;

namespace View
{
    public class LibraryState : IState<ViewState>
    {

        private readonly IWindowController windowController;
        private readonly TargetModel targetModel;
        private readonly IStateMachine<ViewState> viewStateMachine;

        public ViewState Id => ViewState.Library;

        public LibraryState(IWindowController windowController, TargetModel targetModel, IStateMachine<ViewState> viewStateMachine)
        {
            this.windowController = windowController;
            this.targetModel = targetModel;
            this.viewStateMachine = viewStateMachine;
        }

        public void Enter()
        {
            windowController.ShowWindow(typeof(LibraryWindow));
        }

        public void Exit()
        {
            windowController.HideWindow(typeof(LibraryWindow));
        }
    }
}