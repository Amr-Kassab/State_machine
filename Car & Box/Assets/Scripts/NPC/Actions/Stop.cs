using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stop : AIAction
{
    public override void Act(BrainController owner)
    {
        owner.GetComponent<NPC_Main_Controller>().interrepted();
    }
}
