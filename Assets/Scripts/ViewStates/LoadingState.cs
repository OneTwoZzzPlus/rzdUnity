using Interfaces;

namespace ViewStates
{
    public class LoadingState : IState<ViewState>
    {
        public ViewState Id => ViewState.Loading;
        public void Enter()
        {
            throw new System.NotImplementedException();
        }

        public void Exit()
        {
            throw new System.NotImplementedException();
        }
    }
}