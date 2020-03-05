using UnityEngine;

public class HitWall_Keep : MonoBehaviour
{
    public GameObject areaObject;
    public int lastAgentHit;
    public bool net;

    public enum FloorHit
        {
            Service,
            FloorHitUnset,
            FloorAHit,
            FloorBHit
        }

    public FloorHit lastFloorHit;

    private bool firstTouch;

    TennisKeepArea m_Area;
    //TennisAgent m_AgentA;
    //TennisAgent m_AgentB;
    TennisKeepAgent m_Agent;

    //  Use this for initialization
    void Start()
    {
        m_Area = areaObject.GetComponent<TennisKeepArea>();
        m_Agent = m_Area.agent.GetComponent<TennisKeepAgent>();
        firstTouch = true;
    }

    void Reset()
    {
        m_Area.MatchReset();
        lastFloorHit = FloorHit.Service;
        net = false;
        firstTouch = true;
    }
    
    /*void AgentAWins()
    {
        m_AgentA.SetReward(1);
        m_AgentB.SetReward(-1);
        m_AgentA.score += 1;
        Reset();

    }

    void AgentBWins()
    {
        m_AgentA.SetReward(-1);
        m_AgentB.SetReward(1);
        m_AgentB.score += 1;
        Reset();

    }*/

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "iWall")
        {
            m_Agent.SetReward(-1);
            Reset();
        }

        if (collision.gameObject.name == "Floor") {
            //refuerzo negativo
            m_Agent.SetReward(-1);
            Reset();
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
        else if (collision.gameObject.name == "Agent")
        {
            if (!firstTouch) {
                m_Agent.SetReward(1);
            } else if (firstTouch) {
                firstTouch = false;
                m_Agent.SetReward(2);
            }

        }
        /*else if (collision.gameObject.name == "AgentB")
        {
            // Agent B double hit
            if (lastAgentHit == 1)
            {
                AgentAWins();
            }
            else
            {
                if (lastFloorHit != FloorHit.Service && !net)
                {
                    net = true;
                }

                lastAgentHit = 1;
                lastFloorHit = FloorHit.FloorHitUnset;
            }
        }*/
    }
}
