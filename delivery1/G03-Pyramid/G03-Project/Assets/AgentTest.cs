using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentTest : MonoBehaviour
{
    public GameObject wall;
    
    //// Detect when the agent enters the hole
    //private void OnTriggerEnter(Collider col)
    //{
    //    Debug.Log("COLLIDED WITH:");
    //    if (col.gameObject.CompareTag("Hole"))
    //    {
    //        Debug.Log("HOLE");
    //    }

    //    if (col.gameObject.CompareTag("wall"))
    //    {
    //        Debug.Log("WALL");
    //    }

    //    if (col.gameObject.CompareTag("goal"))
    //    {
    //        Debug.Log("GOAL");
    //    }
    //}

    //private void OnTriggerStay(Collider col)
    //{
    //    if (col.gameObject.CompareTag("Hole"))
    //    {
    //        Debug.Log("HOLE");
    //    }

    //    if (col.gameObject.CompareTag("wall"))
    //    {
    //        Debug.Log("WALL");
    //    }

    //    if (col.gameObject.CompareTag("goal"))
    //    {
    //        Debug.Log("GOAL");
    //    }
    //}

    // Update is called once per frame
    void Update()
    {
        if (CheckHoleCollision())
        {
            Debug.Log("HOLEY SHIT");
        }
        else
        {
            Debug.Log("NONE");
        }
    }

    private bool CheckHoleCollision()
    {
        return (transform.position.x > wall.GetComponent<WallHole>().CubeLeft.GetComponent<MeshRenderer>().bounds.center.x
                                 + wall.GetComponent<WallHole>().CubeLeft.GetComponent<MeshRenderer>().bounds.size.x / 2
        && transform.position.x < wall.GetComponent<WallHole>().CubeRight.GetComponent<MeshRenderer>().bounds.center.x
                                 - wall.GetComponent<WallHole>().CubeRight.GetComponent<MeshRenderer>().bounds.size.x / 2
        && transform.position.y > wall.GetComponent<WallHole>().CubeBottom.GetComponent<MeshRenderer>().bounds.center.y
                                 + wall.GetComponent<WallHole>().CubeBottom.GetComponent<MeshRenderer>().bounds.size.y / 2
        && transform.position.y < wall.GetComponent<WallHole>().CubeTop.GetComponent<MeshRenderer>().bounds.center.y
                                 - wall.GetComponent<WallHole>().CubeTop.GetComponent<MeshRenderer>().bounds.size.y / 2
        && transform.position.z < wall.GetComponent<WallHole>().CubeBottom.GetComponent<MeshRenderer>().bounds.center.z
                                 + wall.GetComponent<WallHole>().CubeBottom.GetComponent<MeshRenderer>().bounds.size.z / 2
        && transform.position.z > wall.GetComponent<WallHole>().CubeBottom.GetComponent<MeshRenderer>().bounds.center.z
                                 - wall.GetComponent<WallHole>().CubeBottom.GetComponent<MeshRenderer>().bounds.size.z / 2);
    }
}
