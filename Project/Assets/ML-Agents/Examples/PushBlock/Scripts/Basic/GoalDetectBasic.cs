using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalDetectBasic : MonoBehaviour
{
    /// <summary>
    /// The associated agent.
    /// This will be set by the agent script on Initialization.
    /// Don't need to manually set.
    /// </summary>
    [HideInInspector]
    public PushAgentBasicBasic agent;  //

    void OnCollisionEnter(Collision col)
    {
        // Touched goal.
        if (col.gameObject.CompareTag("goal"))
        {
            agent.ScoredAGoal();
        }
    }
}
