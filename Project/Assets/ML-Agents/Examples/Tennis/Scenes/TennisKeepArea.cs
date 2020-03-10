using UnityEngine;

public class TennisKeepArea : MonoBehaviour
{
    public GameObject ball;
    public GameObject agent;
    Rigidbody m_BallRb;

    // Use this for initialization
    void Start()
    {
        m_BallRb = ball.GetComponent<Rigidbody>();
        MatchReset();
    }

    public void MatchReset()
    {
        var ballOut = Random.Range(-2f, 2f); // distancia en x
        ball.transform.position = new Vector3(ballOut, 6f, 0f) + transform.position;
        m_BallRb.velocity = new Vector3(0f, 0f, 0f); //reset velocidad, hace caida
        ball.transform.localScale = new Vector3(.5f, .5f, .5f);
        ball.GetComponent<HitWall_Keep>().lastAgentHit = -1;
        agent.transform.position = new Vector3(0, -3, 0);
    }

    void FixedUpdate()
    {
        var rgV = m_BallRb.velocity;
        m_BallRb.velocity = new Vector3(Mathf.Clamp(rgV.x, -9f, 9f), Mathf.Clamp(rgV.y, -9f, 9f), rgV.z);
    }
}
