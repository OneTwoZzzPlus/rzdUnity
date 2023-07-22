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
        private readonly SignInventory signInventory;

        private LibraryWindow libraryWindow;

        public ViewState Id => ViewState.Library;

        public LibraryState(SignInventory signInventory, 
                            WindowController windowController, 
                            TargetModel targetModel, 
                            ViewStateMachine viewStateMachine)
        {
            this.signInventory = signInventory;
            this.windowController = windowController;
            this.targetModel = targetModel;
            this.viewStateMachine = viewStateMachine;
        }

        public void Enter()
        {
            libraryWindow = windowController.ShowWindow(typeof(LibraryWindow)) as LibraryWindow;
            libraryWindow.ARButtonClicked += ARButtonClickHandler;
            libraryWindow.SignButtonClicked += SignButtonClickHandler;

            foreach (var signModel in signInventory.GetAll())
            { 
                libraryWindow.UpdateSign(signModel.Id, signModel.Sprite, signModel.IsLocked);
                libraryWindow.SetSignFound(signModel.Id, signModel.UnlockProgress, signModel.IsFound, signModel.FoundTime);
            }
        }

        public void Exit()
        {
            libraryWindow.ARButtonClicked -= ARButtonClickHandler;
            libraryWindow.SignButtonClicked -= SignButtonClickHandler;
            windowController.HideWindow(typeof(LibraryWindow));
        }

        private void ARButtonClickHandler()
        {
            viewStateMachine.ChangeState(ViewState.AR);
        }

        private void SignButtonClickHandler(int id)
        {
            targetModel.Id = id;
            viewStateMachine.ChangeState(ViewState.Info);
        }
    }
}