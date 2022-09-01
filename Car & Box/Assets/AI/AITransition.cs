using System;
using System.Collections.Generic;
using UnityEngine;

public class AITransition : MonoBehaviour
{
    public List<AIDecision> decisions = new List<AIDecision>();
    public AIState newState = null;
    public bool applyTransitionOnZeroDecisions = false;

    //We want to check (go through all decisions) and if one of them fails
    //then we do not want to go to the next state
    //If true is returned, then we can go to the next state
    //if false returned, then we did not go to the next state
    public bool TryTransition(BrainController owner)
    {
        if (decisions.Count == 0)
        {
            if (applyTransitionOnZeroDecisions == true)
            {
                owner.ApplyTransitionToState(newState);
                return true;
            }
            else
            {
                return false;
            }
        }

        foreach (AIDecision d in decisions)
        {
            if (d.IsTrue(owner) == false)
            {
                return false;
            }
        }

        owner.ApplyTransitionToState(newState);
        return true;
    }
    public void OnEnterTransition(BrainController owner)
    {
        foreach (AIDecision d in decisions)
        {
            d.OnEnterDecision(owner);
        }
    }
    public void OnExitTransition(BrainController owner)
    {
        foreach (AIDecision d in decisions)
        {
            d.OnExitDecision(owner);
        }
    }
}