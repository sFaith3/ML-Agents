using UnityEngine;
using MLAgents;

public class Ball3DWithCubeAgent : Agent
{
    [Header("Specific to Ball3D")]
    public GameObject ball;
    public GameObject cube;
    Rigidbody m_BallRb;
    Rigidbody m_CubeRb;
    IFloatProperties m_ResetParams;

    float minReward = 0.1f;
    float maxReward = 0.95f;
    float normalThreshold = 0.5f;
    float maxMoveDist = 2f;

    public override void InitializeAgent()
    {
        m_BallRb = ball.GetComponent<Rigidbody>();
        m_CubeRb = cube.GetComponent<Rigidbody>();
        m_ResetParams = Academy.Instance.FloatProperties;
        SetResetParameters();
    }

    public override void CollectObservations()
    {
        AddVectorObs(gameObject.transform.rotation.z);
        AddVectorObs(gameObject.transform.rotation.x);

        AddVectorObs(gameObject.transform.position.z);
        AddVectorObs(gameObject.transform.position.x);
        AddVectorObs(gameObject.transform.position.y);

        AddVectorObs(ball.transform.position - gameObject.transform.position);
        AddVectorObs(cube.transform.position - gameObject.transform.position);
        AddVectorObs(cube.transform.position - ball.transform.position);

        AddVectorObs(m_BallRb.velocity);
        AddVectorObs(m_CubeRb.velocity);
        AddVectorObs(cube.transform.up);

    }

    public override void AgentAction(float[] vectorAction)
    {
        var actionRotateZ = 2f * Mathf.Clamp(vectorAction[0], -1f, 1f);
        var actionRotateX = 2f * Mathf.Clamp(vectorAction[1], -1f, 1f);
        var actionMoveZ = 0.5f * Mathf.Clamp(vectorAction[2], -1f, 1f);
        var actionMoveX = 0.5f * Mathf.Clamp(vectorAction[3], -1f, 1f);
        var actionMoveY = 0.5f * Mathf.Clamp(vectorAction[4], -1f, 1f);

        //Rotation
        if ((gameObject.transform.rotation.z < 0.25f && actionRotateZ > 0f) ||
            (gameObject.transform.rotation.z > -0.25f && actionRotateZ < 0f))
        {
            gameObject.transform.Rotate(new Vector3(0, 0, 1), actionRotateZ);
        }

        if ((gameObject.transform.rotation.x < 0.25f && actionRotateX > 0f) ||
            (gameObject.transform.rotation.x > -0.25f && actionRotateX < 0f))
        {
            gameObject.transform.Rotate(new Vector3(1, 0, 0), actionRotateX);
        }

        //Movement
        if ((gameObject.transform.localPosition.z < maxMoveDist && actionMoveZ > 0f) ||
            (gameObject.transform.localPosition.z > -maxMoveDist && actionMoveZ < 0f))
        {
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z + actionMoveZ);
        }

        if ((gameObject.transform.localPosition.x < maxMoveDist && actionMoveX > 0f) ||
            (gameObject.transform.localPosition.x > -maxMoveDist && actionMoveX < 0f))
        {
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x + actionMoveX, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z);
        }

        if ((gameObject.transform.localPosition.y < maxMoveDist && actionMoveY > 0f) ||
            (gameObject.transform.localPosition.y > -maxMoveDist && actionMoveY < 0f))
        {
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y + actionMoveY, gameObject.transform.localPosition.z);
        }

        //Rewards
        if ((ball.transform.position.y - gameObject.transform.position.y) < -2f ||
            (cube.transform.position.y - gameObject.transform.position.y) < -2f ||
            Mathf.Abs(ball.transform.position.x - gameObject.transform.position.x) > 3f ||
            Mathf.Abs(ball.transform.position.z - gameObject.transform.position.z) > 3f ||
            cube.transform.position.y - ball.transform.position.y < 1f ||
            Vector3.Dot(cube.transform.up, Vector3.up) < normalThreshold)
        {
            SetReward(-1f);
            Done();
        }
        else
        {
            float xDist = Mathf.Abs((ball.transform.position - transform.position).x);
            float zDist = Mathf.Abs((ball.transform.position - transform.position).z);

            float ballXReward = Mathf.Lerp(maxReward, minReward, (xDist/ 3f));
            float ballZReward = Mathf.Lerp(maxReward, minReward, (zDist / 3f));
            float ballReward = (ballXReward + ballZReward) / 2f;

            float cubeReward = Mathf.Lerp(minReward, maxReward, (Vector3.Dot(cube.transform.up, Vector3.up) - normalThreshold) * 1f/(1f-normalThreshold));

            float headReward = Mathf.Lerp(maxReward, minReward, Mathf.Abs(gameObject.transform.localPosition.y) / maxMoveDist);

            float reward = (ballReward + cubeReward + headReward) / 3f;
            SetReward(reward);
        }
    }

    public override void AgentReset()
    {
        gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
        gameObject.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        gameObject.transform.Rotate(new Vector3(1, 0, 0), Random.Range(-10f, 10f));
        gameObject.transform.Rotate(new Vector3(0, 0, 1), Random.Range(-10f, 10f));
        cube.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);

        m_BallRb.velocity = new Vector3(0f, 0f, 0f);
        m_BallRb.angularVelocity = new Vector3(0f, 0f, 0f);

        m_CubeRb.velocity = new Vector3(0f, 0f, 0f);
        m_CubeRb.angularVelocity = new Vector3(0f, 0f, 0f);

        float xRange = Random.Range(-0.6f, -1.5f);
        float zRange = Random.Range(-1.5f, 1.5f);
        ball.transform.position = new Vector3(xRange, 4f, zRange) + gameObject.transform.position;
        cube.transform.position = new Vector3(xRange, 6f, zRange) + gameObject.transform.position;
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
        m_BallRb.mass = m_ResetParams.GetPropertyWithDefault("mass", 1.0f);
        m_CubeRb.mass = m_ResetParams.GetPropertyWithDefault("mass", 1.0f);
        var scale = m_ResetParams.GetPropertyWithDefault("scale", 1.0f);
        ball.transform.localScale = new Vector3(scale, scale, scale);
    }

    public void SetResetParameters()
    {
        SetBall();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "cube")
        {
            SetReward(-1);
            Done();
        }
    }
}
