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
    public PushAgent_TouchBlock agent;  
    public bool scored = false;
    public bool blockScored = false;

    void OnCollisionEnter(Collision col)
    {
        // Touched goal.
        if (col.gameObject.CompareTag("goal"))
        {
            if (!scored)
            {
                scored = true;
                agent.ScoredAGoal();
            }
        }
        else if (col.gameObject.CompareTag("block")) 
        {
            if (!blockScored)
            {
                blockScored = true;
                agent.ScoredAGoal();
            }
        }
    }
}
