using Data;
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
        private SignDataRegistry signDataRegistry;

        private LibraryWindow libraryWindow;

        public ViewState Id => ViewState.Library;

        public LibraryState(SignDataRegistry signDataRegistry, 
                            WindowController windowController, 
                            TargetModel targetModel, 
                            ViewStateMachine viewStateMachine)
        {
            this.signDataRegistry = signDataRegistry;
            this.windowController = windowController;
            this.targetModel = targetModel;
            this.viewStateMachine = viewStateMachine;
        }

        public void Enter()
        {
            libraryWindow = windowController.ShowWindow(typeof(LibraryWindow)) as LibraryWindow;
            libraryWindow.ARButtonClicked += ARButtonClickHandler;

            foreach (var signData in signDataRegistry.GetAll())
            {
                libraryWindow.CreateSign(signData.Id, signData.Sprite);
            }
        }

        public void Exit()
        {
            libraryWindow.ARButtonClicked -= ARButtonClickHandler;
            windowController.HideWindow(typeof(LibraryWindow));
        }

        private void ARButtonClickHandler()
        {
            viewStateMachine.ChangeState(ViewState.AR);
        }
    }
}