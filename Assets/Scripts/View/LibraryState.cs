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
        private SignInventory signInventory;

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

            foreach (var signData in signInventory.GetAll())
            {
                libraryWindow.CreateSign(signData.Id, signData.Sprite);
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