using System;
using System.Collections;
using UnityEngine;

//abstract keyword
//use it, for a base class, that needs to do a behavior, but multiple scripts
//handle running the appropriate behavior
public abstract class AIDecision : MonoBehaviour
{
    //Abstract means you are forced to add the behavior
    public abstract bool IsTrue(BrainController owner);
    
    //Abstract means you are not forced to add the behavior (its optional)
    public virtual void OnExitDecision(BrainController owner) { }

    public virtual void OnEnterDecision(BrainController owner) { }
}