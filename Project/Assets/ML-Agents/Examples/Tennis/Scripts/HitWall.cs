using UnityEngine;

public class HitWall : MonoBehaviour
{
    public GameObject areaObject;
    public int lastAgentHit;
    public bool net;
    public int currentLoses;

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
        currentLoses = 0;
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
        currentLoses++;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Floor") {
            if (!hasTouchedFloor) {
                hasTouchedFloor = true;
            } else if (hasTouchedFloor) { // segundo bote
                if (firstTouch) {
                    if (currentLoses >= 2500) m_Agent.AddReward(-4); //currentReward -= 3;
                    else if(currentLoses <2500) m_Agent.AddReward(-1); //currentReward--; // Extra if no touch
                }
                //m_Agent.AddReward(currentReward - 1);
                m_Agent.AddReward(-1);
                Reset();
            }
            
        } else if(collision.gameObject.name == "WallFront") {
            if(hasTouchedAgent) hasTouchedWall = true;
        } else if (collision.gameObject.name == "Agent") { // Toca raqueta
            if (firstTouch) {
                //currentReward += 2; // Boost al principio
                m_Agent.AddReward(2);
                firstTouch = false;
            } else if (hasTouchedWall && hasTouchedFloor) {
                hasTouchedWall = false;
                hasTouchedFloor = false;
                //currentReward++;
                m_Agent.AddReward(1);
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
