using System;
using UnityEngine;

public class BrainController : MonoBehaviour
{
    public AIState initialState = null;
    public AIState currentState = null;

    void Start()
    {
        if (initialState != null)
        {
            ApplyTransitionToState(initialState);
        }
    }
    void Update()
    {
        if (currentState != null)
        {
            currentState.RunState(this);
        }
    }

    public void ApplyTransitionToState(AIState newState)
    {
        if (newState == currentState)
        {
            return;
        }
        if(currentState != null)
        {
            currentState.ExitState(this);
        }
        currentState = newState;
        if(currentState != null)
        {
            currentState.EnterState(this);
        }
    }
}