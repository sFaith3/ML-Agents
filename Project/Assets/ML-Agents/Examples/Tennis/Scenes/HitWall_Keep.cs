using UnityEngine;

public class HitWall_Keep : MonoBehaviour
{
    public GameObject areaObject;
    public int lastAgentHit;
    public bool net;

    TennisKeepArea m_Area;
    TennisKeepAgent m_Agent;

    public bool EX3 = false;

    void Start()
    {
        m_Area = areaObject.GetComponent<TennisKeepArea>();
        m_Agent = m_Area.agent.GetComponent<TennisKeepAgent>();
    }

    /*if (collision.gameObject.CompareTag("iWall"))
{
    if (collision.gameObject.name == "wallA")
    {
        // Agent A hits into wall or agent B hit a winner
        if (lastAgentHit == 0 || lastFloorHit == FloorHit.FloorAHit)
        {
            AgentBWins();
        }
        // Agent B hits long
        else
        {
            AgentAWins();
        }
    }
    else if (collision.gameObject.name == "wallB")
    {
        // Agent B hits into wall or agent A hit a winner
        if (lastAgentHit == 1 || lastFloorHit == FloorHit.FloorBHit)
        {
            AgentAWins();
        }
        // Agent A hits long
        else
        {
            AgentBWins();
        }
    }
    else if (collision.gameObject.name == "floorA")
    {
        // Agent A hits into floor, double bounce or service
        if (lastAgentHit == 0 || lastFloorHit == FloorHit.FloorAHit || lastFloorHit == FloorHit.Service)
        {
            AgentBWins();
        }
        else
        {
            lastFloorHit = FloorHit.FloorAHit;
            //successful serve
            if (!net)
            {
                net = true;
            }
        }
    }
    else if (collision.gameObject.name == "floorB")
    {
        // Agent B hits into floor, double bounce or service
        if (lastAgentHit == 1 || lastFloorHit == FloorHit.FloorBHit || lastFloorHit == FloorHit.Service)
        {
            AgentAWins();
        }
        else
        {
            lastFloorHit = FloorHit.FloorBHit;
            //successful serve
            if (!net)
            {
                net = true;
            }
        }
    }
    else if (collision.gameObject.name == "net" && !net)
    {
        if (lastAgentHit == 0)
        {
            AgentBWins();
        }
        else if (lastAgentHit == 1)
        {
            AgentAWins();
        }
    }
}*/

    void Reset()
    {
        m_Area.MatchReset();
        m_Agent.Done();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "iWall")
        {
            m_Agent.AddReward(-0.5f);
            if (!EX3)
            {
                Reset();
            } 
            else
            {
                gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
           
        }
        if (collision.gameObject.tag == "Agent")
        {
            m_Agent.AddReward(2);
        }
        if(collision.gameObject.name == "Floor")
        {
            m_Agent.AddReward(-1);
            Reset();
        }

    }
}
