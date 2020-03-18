using UnityEngine;
using UnityEngine.UI;
using MLAgents;

public class TennisKeepAgent : Agent
{
    [Header("Specific to Tennis")]
    public GameObject ball;
    public bool invertX = true;
    public int score;
    public GameObject myArea;
    public float angle;
    public float scale;

    Text m_TextComponent;
    Rigidbody m_AgentRb;
    Rigidbody m_BallRb;
    float m_InvertMult;
    IFloatProperties m_ResetParams;

    // Looks for the scoreboard based on the name of the gameObjects.
    // Do not modify the names of the Score GameObjects
    const string k_CanvasName = "Canvas";
    const string k_ScoreBoardAName = "ScoreA";
    const string k_ScoreBoardBName = "ScoreB";

    private float distance = 0.0f;

    public bool EX3 = false;
    float moveX, moveY, moveZ;

    public override void InitializeAgent()
    {
        m_AgentRb = GetComponent<Rigidbody>();
        m_BallRb = ball.GetComponent<Rigidbody>();
        var canvas = GameObject.Find(k_CanvasName);
        GameObject scoreBoard;
        m_ResetParams = Academy.Instance.FloatProperties;
        if (invertX)
        {
            scoreBoard = canvas.transform.Find(k_ScoreBoardBName).gameObject;
        }
        else
        {
            scoreBoard = canvas.transform.Find(k_ScoreBoardAName).gameObject;
        }
        m_TextComponent = scoreBoard.GetComponent<Text>();
        SetResetParameters();
    }

    public override void CollectObservations()
    {
        AddVectorObs(m_InvertMult * (transform.position.x - myArea.transform.position.x));
        AddVectorObs(transform.position.y - myArea.transform.position.y);
        AddVectorObs(m_InvertMult * m_AgentRb.velocity.x);
        AddVectorObs(m_AgentRb.velocity.y);

        AddVectorObs(m_InvertMult * (ball.transform.position.x - myArea.transform.position.x));
        AddVectorObs(ball.transform.position.y - myArea.transform.position.y);
        AddVectorObs(m_InvertMult * m_BallRb.velocity.x);
        AddVectorObs(m_BallRb.velocity.y);

        AddVectorObs(m_InvertMult * gameObject.transform.rotation.x);
        AddVectorObs(distance);
    }

    public override void AgentAction(float[] vectorAction)
    {
        //Vector3 dirToGo = Vector3.zero;
        var rotate = Mathf.Clamp(vectorAction[2], -1f, 1f);
        if (EX3)
        {
            moveX = vectorAction[0];
            moveY = Mathf.Clamp(vectorAction[1], -1f, 1f);
            moveZ = vectorAction[2];
        } 
        else
        {
            moveX = Mathf.Clamp(vectorAction[0], -1f, 1f) /** m_InvertMult*/;
            moveY = Mathf.Clamp(vectorAction[1], -1f, 1f);
            moveZ = vectorAction[2];
        }
 
        //var rotate = Mathf.Clamp(0f, -1f, 1f) * m_InvertMult;

        ////
        if (moveY > 0.5 && transform.position.y - transform.parent.transform.position.y < -1.5f)
        {
            m_AgentRb.velocity = new Vector3(m_AgentRb.velocity.x, 7f, 0f);
        }

        if(!EX3) ///EX2
        {
            Vector3 dir = ball.transform.position - transform.position;
            dir.Normalize();

            //tensorboard --logdir=summaries --port=6006
            //mlagents-learn config/trainer_config.yaml --run-id=314 --train

            transform.eulerAngles = new Vector3(-180.0f, -180.0f, 0.0f);

            distance = ball.transform.position.x - transform.position.x;
            distance = Mathf.Abs(distance);
            print(distance);

            if (distance < 2.0f)
            {
                AddReward(1);
            }
            else
            {
                AddReward(-1);
            }

            m_AgentRb.velocity = new Vector3(moveX * dir.x * 30.0f, m_AgentRb.velocity.y, 0f);
        } 
        else ///EX3
        {
            Vector3 dir = ball.transform.position - transform.position;
            float magnitude = dir.magnitude * 30.0f;
            dir.Normalize();
            
            //tensorboard --logdir=summaries --port=6006
            //mlagents-learn config/trainer_config.yaml --run-id=314 --train

            //transform.eulerAngles = new Vector3(-180.0f, -180.0f, 0.0f);

            //distance = ball.transform.position.x - transform.position.x;
            float distance1 = ball.transform.position.x - transform.position.x;
            float distance2 = ball.transform.position.z - transform.position.z;

            distance1 = Mathf.Abs(distance);
            distance2 = Mathf.Abs(distance);
            print(distance2);         

            if (distance1 < 2.0f)
            {
                AddReward(0.5f);
            }
            else
            {
                AddReward(-0.5f);
            }

            if (distance2 < 2.0f)
            {
                AddReward(0.5f);
            }
            else
            {
                AddReward(-0.5f);
            }

            m_AgentRb.velocity = new Vector3(moveX * dir.x * magnitude, m_AgentRb.velocity.y, moveX * dir.z * magnitude);
            m_AgentRb.transform.rotation = Quaternion.Euler(-180f, -180f, 55f * rotate);
        }
        

        //m_AgentRb.velocity = new Vector3(moveX * 30.0f, m_AgentRb.velocity.y, 0f);
        ////m_AgentRb.transform.rotation = Quaternion.Euler(55.0f * rotate + m_InvertMult * 180.0f, 0.0f, 0.0f);
        //m_AgentRb.transform.rotation = Quaternion.Euler(55.0f * rotate + m_InvertMult * 180.0f, -180.0f, 0.0f);

        //if (invertX && transform.position.x - transform.parent.transform.position.x < -m_InvertMult ||
        //    !invertX && transform.position.x - transform.parent.transform.position.x > -m_InvertMult)
        //{
        //    transform.position = new Vector3(-m_InvertMult + transform.parent.transform.position.x,
        //        transform.position.y,
        //        transform.position.z);
        //}

        m_TextComponent.text = score.ToString();
    }

    public override float[] Heuristic()
    {
        var action = new float[2];

        action[0] = Input.GetAxis("Horizontal");
        action[1] = Input.GetKey(KeyCode.Space) ? 1f : 0f;
        return action;
    }

    public override void AgentReset()
    {
        //m_InvertMult = invertX ? -1f : 1f;

        transform.position = new Vector3(1.0f, -4.5f, 1.5f);
        m_AgentRb.velocity = new Vector3(0f, 0f, 0f);

        SetResetParameters();
    }

    public void SetRacket()
    {
        angle = m_ResetParams.GetPropertyWithDefault("angle", 55);
        gameObject.transform.eulerAngles = new Vector3(
            m_InvertMult * angle,                               // Invertido en la X en vez que en la Z
            gameObject.transform.eulerAngles.y,
            gameObject.transform.eulerAngles.z
        );
    }

    public void SetBall()
    {
        scale = m_ResetParams.GetPropertyWithDefault("scale", .5f);
        ball.transform.localScale = new Vector3(scale, scale, scale);
    }

    public void SetResetParameters()
    {
        SetRacket();
        SetBall();
    }
}
