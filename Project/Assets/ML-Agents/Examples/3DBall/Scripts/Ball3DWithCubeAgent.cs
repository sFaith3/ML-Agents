using UnityEngine;
using MLAgents;

public class Ball3DWithCubeAgent : Agent
{
    [Header("Specific to Ball3D")]
    public GameObject ball1;
    public GameObject ball2;
    Rigidbody m_BallRb1;
    Rigidbody m_BallRb2;
    IFloatProperties m_ResetParams;
    float minBallDist = 1.2f;
    float maxBallDist = 6.0f;

    public override void InitializeAgent()
    {
        m_BallRb1 = ball1.GetComponent<Rigidbody>();
        m_BallRb2 = ball2.GetComponent<Rigidbody>();
        m_ResetParams = Academy.Instance.FloatProperties;
        SetResetParameters();
    }

    public override void CollectObservations()
    {
        AddVectorObs(gameObject.transform.rotation.z);
        AddVectorObs(gameObject.transform.rotation.x);

        AddVectorObs(ball1.transform.position - gameObject.transform.position);
        AddVectorObs(ball2.transform.position - gameObject.transform.position);
        AddVectorObs(ball1.transform.position - ball2.transform.position);
        
        AddVectorObs(m_BallRb1.velocity);
        AddVectorObs(m_BallRb2.velocity);
    }

    public override void AgentAction(float[] vectorAction)
    {
        var actionZ = 2f * Mathf.Clamp(vectorAction[0], -1f, 1f);
        var actionX = 2f * Mathf.Clamp(vectorAction[1], -1f, 1f);

        if ((gameObject.transform.rotation.z < 0.25f && actionZ > 0f) ||
            (gameObject.transform.rotation.z > -0.25f && actionZ < 0f))
        {
            gameObject.transform.Rotate(new Vector3(0, 0, 1), actionZ);
        }

        if ((gameObject.transform.rotation.x < 0.25f && actionX > 0f) ||
            (gameObject.transform.rotation.x > -0.25f && actionX < 0f))
        {
            gameObject.transform.Rotate(new Vector3(1, 0, 0), actionX);
        }
        if ((ball1.transform.position.y - gameObject.transform.position.y) < -2f ||
            (ball2.transform.position.y - gameObject.transform.position.y) < -2f ||
            Mathf.Abs(ball1.transform.position.x - gameObject.transform.position.x) > 3f ||
            Mathf.Abs(ball2.transform.position.x - gameObject.transform.position.x) > 3f ||
            Mathf.Abs(ball1.transform.position.z - gameObject.transform.position.z) > 3f ||
            Mathf.Abs(ball2.transform.position.z - gameObject.transform.position.z) > 3f)
        {
            SetReward(-1f);
            Done();
        }
        else
        {
            
            float reward = Mathf.Lerp(0.95f,0.1f,(Mathf.Clamp(Vector3.Distance(ball1.transform.position, ball2.transform.position),minBallDist,maxBallDist) -minBallDist) / maxBallDist);
            SetReward(reward);
        }
    }

    public override void AgentReset()
    {
        gameObject.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        gameObject.transform.Rotate(new Vector3(1, 0, 0), Random.Range(-10f, 10f));
        gameObject.transform.Rotate(new Vector3(0, 0, 1), Random.Range(-10f, 10f));
        m_BallRb1.velocity = new Vector3(0f, 0f, 0f);
        m_BallRb2.velocity = new Vector3(0f, 0f, 0f);
        ball1.transform.position = new Vector3(Random.Range(-0.6f, -1.5f), 4f, Random.Range(-1.5f, 1.5f))
            + gameObject.transform.position;
        ball2.transform.position = new Vector3(Random.Range(0.6f, 1.5f), 4f, Random.Range(-1.5f, 1.5f))
            + gameObject.transform.position;
        //Reset the parameters when the Agent is reset.
        SetResetParameters();
    }

    public override float[] Heuristic()
    {
        var action = new float[2];

        action[0] = -Input.GetAxis("Horizontal");
        action[1] = Input.GetAxis("Vertical");
        return action;
    }

    public void SetBall()
    {
        //Set the attributes of the ball by fetching the information from the academy
        m_BallRb1.mass = m_ResetParams.GetPropertyWithDefault("mass", 1.0f);
        m_BallRb2.mass = m_ResetParams.GetPropertyWithDefault("mass", 1.0f);
        var scale = m_ResetParams.GetPropertyWithDefault("scale", 1.0f);
        ball1.transform.localScale = new Vector3(scale, scale, scale);
        ball2.transform.localScale = new Vector3(scale, scale, scale);
    }

    public void SetResetParameters()
    {
        SetBall();
    }
}
