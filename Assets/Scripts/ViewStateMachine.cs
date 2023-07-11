using System.Collections.Generic;
using Interfaces;

public class ViewStateMachine : IStateMachine<ViewState>
{
    public IState<ViewState> CurrentState => states[currentStateId];
    
    private ViewState currentStateId;
    private ViewState previousStateId;


    private readonly Dictionary<ViewState, IState<ViewState>> states;

    public ViewStateMachine(IState<ViewState>[] states, ViewState initialStateID)
    {
        this.states = new Dictionary<ViewState, IState<ViewState>>();
        foreach (var state in states) this.states.Add(state.Id, state);

        previousStateId = initialStateID;
        currentStateId = initialStateID;
        CurrentState.Enter();
    }

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