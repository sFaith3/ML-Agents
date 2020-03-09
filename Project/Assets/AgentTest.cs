using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentTest : MonoBehaviour
{
    // Detect when the agent enters the hole
    private void OnTriggerEnter(Collider col)
    {
        Debug.Log("COLLIDED WITH:");
        if (col.gameObject.CompareTag("Hole"))
        {
            Debug.Log("HOLE");
        }

        if (col.gameObject.CompareTag("wall"))
        {
            Debug.Log("WALL");
        }

        if (col.gameObject.CompareTag("goal"))
        {
            Debug.Log("GOAL");
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("Hole"))
        {
            Debug.Log("HOLE");
        }

        if (col.gameObject.CompareTag("wall"))
        {
            Debug.Log("WALL");
        }

        if (col.gameObject.CompareTag("goal"))
        {
            Debug.Log("GOAL");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
