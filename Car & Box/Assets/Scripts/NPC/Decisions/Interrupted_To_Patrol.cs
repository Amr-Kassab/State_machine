using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interrupted_To_Patrol : AIDecision
{
    public override bool IsTrue(BrainController owner)
    {
        return !owner.GetComponent<NPC_Main_Controller>().CheckIfPlayerIsInTheWay();
    }
}
