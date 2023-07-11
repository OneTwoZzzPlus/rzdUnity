using System.Collections.Generic;
using Interfaces;

namespace View
{
    public class ViewStateMachine : IStateMachine<ViewState>
    {
        private readonly Dictionary<ViewState, IState<ViewState>> states;

        private ViewState currentStateId;
        private ViewState previousStateId;

        public ViewStateMachine(IState<ViewState>[] states, ViewState initialStateID)
        {
            this.states = new Dictionary<ViewState, IState<ViewState>>();
            foreach (var state in states) this.states.Add(state.Id, state);

            previousStateId = initialStateID;
            currentStateId = initialStateID;
            CurrentState.Enter();
        }

        public IState<ViewState> CurrentState => states[currentStateId];

        public void ChangeState(ViewState desiredStateId)
        {
            if (desiredStateId.Equals(currentStateId)) return;

            previousStateId = currentStateId;
            CurrentState.Exit();
            currentStateId = desiredStateId;
            CurrentState.Enter();
        }

        public void RevertState()
        {
            ChangeState(previousStateId);
        }
    }
}