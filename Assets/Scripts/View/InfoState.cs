using DefaultNamespace;
using Interfaces;
using Model;

namespace View
{
    public class InfoState : IState<ViewState>
    {
        private readonly IWindowController windowController;
        private readonly TargetModel targetModel;
        private readonly IStateMachine<ViewState> viewStateMachine;

        public ViewState Id => ViewState.Info;

        public InfoState(IWindowController windowController, TargetModel targetModel, IStateMachine<ViewState> viewStateMachine)
        {
            this.windowController = windowController;
            this.targetModel = targetModel;
            this.viewStateMachine = viewStateMachine;
        }

        public void Enter()
        {
            windowController.ShowWindow(typeof(SignInfoWindow));
        }

        public void Exit()
        {
            windowController.HideWindow(typeof(SignInfoWindow));
        }
    }
}