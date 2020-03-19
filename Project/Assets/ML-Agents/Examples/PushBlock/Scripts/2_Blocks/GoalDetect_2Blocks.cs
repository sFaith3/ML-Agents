using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalDetect_2Blocks : MonoBehaviour
{
    /// <summary>
    /// The associated agent.
    /// This will be set by the agent script on Initialization.
    /// Don't need to manually set.
    /// </summary>
    [HideInInspector]
    public PushAgent_2Blocks agent;  //
    public bool scored = false;

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
    }
}
