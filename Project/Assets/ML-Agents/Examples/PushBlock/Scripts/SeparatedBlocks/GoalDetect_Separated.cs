﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalDetect_Separated : MonoBehaviour
{
    /// <summary>
    /// The associated agent.
    /// This will be set by the agent script on Initialization.
    /// Don't need to manually set.
    /// </summary>
    [HideInInspector]
    public PushAgent_Separated agent;  //
    public bool scored = false;

    void OnCollisionEnter(Collision col)
    {
        string myTag = transform.tag;

        // Touched goal.
        if (myTag == "greenCube") 
        {
            if (col.gameObject.CompareTag("greenGoal"))
            {
                if (!scored)
                {
                    scored = true;
                    agent.ScoredAGoal();
                }
            }
            else if(col.gameObject.CompareTag("purpleGoal"))
            {
                agent.AddReward((float)-0.5);
                agent.Done();   
            }
        }
        else if(myTag == "purpleCube")
        {
            if (col.gameObject.CompareTag("purpleGoal"))
            {
                if (!scored)
                {
                    scored = true;
                    agent.ScoredAGoal();
                }
            }
            else if (col.gameObject.CompareTag("greenGoal"))
            {
                agent.AddReward((float)-0.5);
                agent.Done();
            }
        }
    }
}
