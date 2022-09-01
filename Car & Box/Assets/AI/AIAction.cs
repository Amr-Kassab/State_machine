using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//AIA = AIAction
public abstract class AIAction : MonoBehaviour
{
    public abstract void Act(BrainController owner);
    public virtual void OnEnterAction(BrainController owner) { }
    internal virtual void OnExitAction(BrainController owner) { }
}