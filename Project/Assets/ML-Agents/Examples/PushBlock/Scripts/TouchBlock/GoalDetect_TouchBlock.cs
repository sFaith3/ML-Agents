using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalDetect_TouchBlock : MonoBehaviour
{
    /// <summary>
    /// The associated agent.
    /// This will be set by the agent script on Initialization.
    /// Don't need to manually set.
    /// </summary>
    [HideInInspector]
    public PushAgent_TouchBlock agent;  //
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
                    agent.ScoredAGoal(PushAgent_TouchBlock.AgentGoal.SECOND_GOAL,true, col.gameObject.tag);
                }
            }
            else if(col.gameObject.CompareTag("purpleGoal"))
            {
                agent.ScoredAGoal(PushAgent_TouchBlock.AgentGoal.TOUCH_CUBE, false, col.gameObject.tag);
                agent.Done();   
            }
            else if (col.gameObject.CompareTag("purpleCube")) 
            {
                agent.ScoredAGoal(PushAgent_TouchBlock.AgentGoal.FIRST_GOAL, true, col.gameObject.tag);
            }
        }
        else if(myTag == "purpleCube")
        {
            if (col.gameObject.CompareTag("purpleGoal"))
            {
                if (!scored)
                {
                    scored = true;
                    agent.ScoredAGoal(PushAgent_TouchBlock.AgentGoal.SECOND_GOAL, true, col.gameObject.tag);
                }
            }
            else if (col.gameObject.CompareTag("greenGoal"))
            {
                agent.ScoredAGoal(PushAgent_TouchBlock.AgentGoal.TOUCH_CUBE, false, col.gameObject.tag);
                agent.Done();
            }
            else if (col.gameObject.CompareTag("greenCube"))
            {
                agent.ScoredAGoal(PushAgent_TouchBlock.AgentGoal.FIRST_GOAL, true, col.gameObject.tag);
            }
        }
    }
}
