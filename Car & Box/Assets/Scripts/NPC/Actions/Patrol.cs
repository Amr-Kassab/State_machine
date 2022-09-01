using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : AIAction
{
    public override void Act(BrainController owner)
    {
        owner.GetComponent<NPC_Main_Controller>().continueMoving();
        owner.GetComponent<NPC_Main_Controller>().Patrol();
    }
}
