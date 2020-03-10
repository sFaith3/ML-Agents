using UnityEngine;

public class HitWall : MonoBehaviour
{
    public GameObject areaObject;
    public int lastAgentHit;
    public bool net;

    private int currentReward;

    private bool hasTouchedFloor;
    private bool hasTouchedWall;
    private bool hasTouchedAgent;
    private bool firstTouch;


    TennisArea m_Area;
    TennisAgent m_Agent;

    //  Use this for initialization
    void Start()
    {
        m_Area = areaObject.GetComponent<TennisArea>();
        m_Agent = m_Area.agent.GetComponent<TennisAgent>();
        hasTouchedFloor = false;
        hasTouchedWall = false;
        hasTouchedAgent = false;
        firstTouch = true;
        currentReward = 0;
    }

    void Reset()
    {
        m_Agent.Done();
        m_Area.MatchReset();
        net = false;
        hasTouchedFloor = false;
        hasTouchedWall = false;
        firstTouch = true;
        currentReward = 0;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Floor") {
            if (!hasTouchedFloor) {
                hasTouchedFloor = true;
            } else if (hasTouchedFloor) { // segundo bote
                if (firstTouch) currentReward--; // Extra if no touch
                m_Agent.SetReward(currentReward - 1);
                Reset();
            }
            
        } else if(collision.gameObject.name == "WallFront") {
            if(hasTouchedAgent) hasTouchedWall = true;
        } else if (collision.gameObject.name == "Agent") { // Toca raqueta
            if (firstTouch) {
                currentReward += 2; // Boost al principio
            } else if (hasTouchedWall && hasTouchedFloor) {
                hasTouchedWall = false;
                hasTouchedFloor = false;
                currentReward++;
            }
            hasTouchedAgent = true;
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
