using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingPlat : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 40000, 0));
            Debug.Log("Voladore");
        }
    }
}
