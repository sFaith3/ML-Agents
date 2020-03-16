using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PyramidScript : MonoBehaviour
{
    public GameObject button;

    // Start is called before the first frame update
    void Start()
    {
        button = GameObject.FindGameObjectWithTag("switchOff");
    }


    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("agent"))
        {
            button.gameObject.tag = "goal";
        }
    }
}
