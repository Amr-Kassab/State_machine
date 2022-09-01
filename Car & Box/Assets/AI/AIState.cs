using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIState : MonoBehaviour
{
    public List<AIAction> actions = new List<AIAction>();
    public List<AITransition> transitions = new List<AITransition>();

    public void EnterState(BrainController owner)
    {
        foreach (AIAction a in actions)
        {
            a.OnEnterAction(owner);
        }
        foreach (AITransition t in transitions)
        {
            t.OnEnterTransition(owner);
        }
    }
    public void RunState(BrainController owner)
    {
        foreach (AIAction a in actions)
        {
            a.Act(owner);
        }
        foreach (AITransition t in transitions)
        {
            if (t.TryTransition(owner))
            {
                return;
            }
        }
    }
    public void ExitState(BrainController owner)
    {
        foreach (AIAction a in actions)
        {
            a.OnExitAction(owner);
        }
        foreach (AITransition t in transitions)
        {
            t.OnExitTransition(owner);
        }
    }
}