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

    private enum Status {
        Floor, Agent, Wall
    };
    Status state;

    TennisArea m_Area;
    TennisAgent m_Agent;

    //  Use this for initialization
    void Start()
    {
        m_Area = areaObject.GetComponent<TennisArea>();
        m_Agent = m_Area.agent.GetComponent<TennisAgent>();
        hasTouchedFloor = true;
        hasTouchedWall = false;
        hasTouchedAgent = false;
        firstTouch = true;
        currentReward = 0;
        currentLoses = 0;
        state = Status.Floor;
    }

    void Reset()
    {
        m_Agent.Done();
        m_Area.MatchReset();
        net = false;
        hasTouchedFloor = true; // Evita que haga dos botes al principio
        hasTouchedWall = false;
        firstTouch = true;
        currentReward = 0;
        currentLoses++;
        state = Status.Floor;
    }

    void OnCollisionEnter(Collision collision)
    {
        /*if (collision.gameObject.name == "Floor") {
            if (hasTouchedWall) {
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
            if (hasTouchedAgent) {
                hasTouchedWall = true;
            }
        } else if (collision.gameObject.name == "Agent") { // Toca raqueta
            if (firstTouch) {
                //currentReward += 2; // Boost al principio
                m_Agent.AddReward(2);
                firstTouch = false;
                hasTouchedAgent = true;
            } else if (hasTouchedWall && hasTouchedFloor) {
                hasTouchedWall = false;
                //currentReward++;
                m_Agent.AddReward(1);
                hasTouchedAgent = true;
            }
            
        }*/

        if (collision.gameObject.name == "Floor") {
            switch (state) {
                case Status.Floor: // Doble bote en el suelo
                    Death(-1);
                    break;

                case Status.Agent: // De raqueta a suelo
                    Death(-1);
                    break;

                case Status.Wall:
                    state = Status.Floor;
                    break;
            }
        }
        else if (collision.gameObject.name == "WallFront") {
            switch (state) {
                case Status.Floor:
                    Death(-1);
                    break;

                case Status.Agent:
                    state = Status.Agent;
                    break;

                case Status.Wall: // De pared a pared
                    Death(-5);
                    break;
            }
        }
        else if (collision.gameObject.name == "Agent") {
            switch (state) {
                case Status.Floor: // Le da tras un bote
                    if (firstTouch) {
                        m_Agent.AddReward(2);
                        firstTouch = false;
                    } else if (!firstTouch) {
                        m_Agent.AddReward(1);
                    }
                    state = Status.Agent;
                    break;

                case Status.Agent: // Doble hit
                    Death(-0.5f);
                    break;

                case Status.Wall: // Hit directo, ha de esperar a que toque el suelo
                    Death(-0.5f);
                    break;
            }

        }

        

    }

    public void Death(float extra) {
        if (firstTouch) {
            if (currentLoses >= 2500) m_Agent.AddReward(-4); //currentReward -= 3;
            else if (currentLoses < 2500) m_Agent.AddReward(-1); //currentReward--; // Extra if no touch
        }
        //m_Agent.AddReward(currentReward - 1);
        m_Agent.AddReward(extra);
        Reset();
    }
}
