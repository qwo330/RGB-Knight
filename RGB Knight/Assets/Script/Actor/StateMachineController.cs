using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineController : MonoBehaviour
{
    [SerializeField] private List<StateController> states;

    private StateController current;

    private void Awake()
    {
        InitStateMachine();
    }

    private void InitStateMachine()
    {
        foreach (var state in states)
        {
            state.InvokeTransition = ChangeState;
            if (state.Active)
                current = state;
        }
    }

    public void ChangeState(System.Type target)
    {
        var next = states.Find(x => x.GetType() == target);
        current.Inactivate();
        next.Activate();
        current = next;
    }
}