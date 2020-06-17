using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raycastTest : MonoBehaviour
{
    RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Physics.Raycast(transform.position, Vector3.forward, out hit, 50f))
        {
            if (hit.transform.CompareTag("wall"))
            {
                Debug.Log("WALL");
            }
        }
        else
        {
            Debug.Log("NONE");
        }
    }
}
