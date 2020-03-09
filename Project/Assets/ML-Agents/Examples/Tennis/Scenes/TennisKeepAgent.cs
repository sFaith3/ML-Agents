﻿using UnityEngine;
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
    }

    public override void AgentAction(float[] vectorAction)
    {
        Vector3 dirToGo = Vector3.zero;
        var moveX = /*Mathf.Clamp(*/vectorAction[0] * m_InvertMult;
        var moveY = Mathf.Clamp(vectorAction[1], -1f, 1f);
        //var rotate = Mathf.Clamp(0f, -1f, 1f) * m_InvertMult;

        //print(new Vector2(moveX, moveY) + m_InvertMult.ToString());

        ////
        if (moveY > 0.5 && transform.position.y - transform.parent.transform.position.y < -1.5f)
        {
            m_AgentRb.velocity = new Vector3(m_AgentRb.velocity.x, 7f, 0f);
        }

        //switch (vectorAction[0])
        //{
        //    case 1:
        //        dirToGo = transform.forward * 10.0f;
        //        break;
        //}

        //switch (vectorAction[1])
        //{
        //    case 1:
        //        dirToGo = transform.right * 3f;
        //        break;
        //    case 2:
        //        dirToGo = transform.right * -3f;
        //        break;
        //}

        //float distance = Vector3.Distance(ball.gameObject.transform.position, transform.position);
        //if (vectorAction[0] > 0.0f)
        //    gameObject.GetComponent<Rigidbody>().AddForce(Vector3.right * 2.0f, ForceMode.Impulse);
        //else if(vectorAction[0] < 0.0f)
        //    gameObject.GetComponent<Rigidbody>().AddForce(Vector3.right * -2.0f, ForceMode.Impulse);
        //if (vectorAction[1] > 0.0f)
        //    gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 1.0f, ForceMode.Impulse);
        //else if (vectorAction[1] < 0.0f)
        //    gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * -1.0f, ForceMode.Impulse);

        //m_AgentRb.AddForce(dirToGo, ForceMode.VelocityChange);
        m_AgentRb.velocity = new Vector3(moveX * 30.0f, m_AgentRb.velocity.y, 0f);

        transform.eulerAngles = new Vector3(-180.0f, -180.0f, 0.0f);











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
        m_InvertMult = invertX ? -1f : 1f;

        transform.position = new Vector3(0.0f, -4.5f, 1.5f) + transform.parent.transform.position;
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
