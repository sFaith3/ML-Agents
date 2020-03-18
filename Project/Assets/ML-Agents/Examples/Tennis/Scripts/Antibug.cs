using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Antibug : MonoBehaviour
{
    public GameObject ball;
    Rigidbody m_BallRb;

    private bool counter;
    private float startTime;
    private float elapsedTime;


    // Start is called before the first frame update
    void Start()
    {
        counter = false;
        m_BallRb = ball.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (counter) {
            elapsedTime = Time.time - startTime;
            if(elapsedTime > 5.0f) {
                ball.GetComponent<HitWall>().Death(-5);
  
                elapsedTime = 0.0f;
                counter = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        startTime = Time.time;
        counter = true;
    }

    private void OnTriggerExit(Collider other)
    {
        elapsedTime = 0.0f;
        counter = false;
    }
}
