using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol_TO_Interrupt : AIDecision
{
    public override bool IsTrue(BrainController owner)
    {
       return owner.GetComponent<NPC_Main_Controller>().CheckIfPlayerIsInTheWay();
    }
}
