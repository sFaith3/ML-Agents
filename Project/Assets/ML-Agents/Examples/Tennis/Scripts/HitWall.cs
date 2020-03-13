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
    
    private float checkLostGames = 1500f;
    private float count = 0;
    private float goodReward = 5;

    private enum Status {
        Service, Floor, Agent, Wall
    };
    Status state;

    TennisArea m_Area;
    TennisAgent m_Agent;

    //  Use this for initialization
    void Start()
    {
        m_Area = areaObject.GetComponent<TennisArea>();
        m_Agent = m_Area.agent.GetComponent<TennisAgent>();
        //hasTouchedFloor = true;
        //hasTouchedWall = false;
        //hasTouchedAgent = false;
        //firstTouch = true;
        currentReward = 0;
        currentLoses = 0;
        state = Status.Service;
        count = 0;
    }

    void Reset()
    {
        m_Agent.Done();
        m_Area.MatchReset();
        net = false;
        //hasTouchedFloor = false; // Evita que haga dos botes al principio //estaba en true
        //hasTouchedWall = false;
        //firstTouch = true;
        currentReward = 0;
        currentLoses++;
        state = Status.Service;
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
            switch (state)
            {
                case Status.Service: //saque
                    //if (CheckLostTooMuchGames(checkLostGames)) Death(-4); //poner negativo mas potente?
                    //else Death(-1);
                    count++;
                    Death(-count * 4);
                    break;

                case Status.Floor: // Doble bote en el suelo
                    Death(-1);
                    break;

                case Status.Agent: // De raqueta a suelo
                    Death(-1);
                    break;

                case Status.Wall:
                    //WellDone(0.5f);//No hace falta pq no es accion d la IA
                    state = Status.Floor;
                    break;
            }
        }
        else if (collision.gameObject.name == "WallFront") {
            switch (state) {
                case Status.Service: //no deberia ocurrir pero por si aka
                    //if (CheckLostTooMuchGames(checkLostGames)) Death(-4); 
                    //else Death(-1);
                    Death(-3);
                    break;

                case Status.Floor:
                    Death(-1);
                    break;

                case Status.Agent:
                    WellDone(2);
                    state = Status.Wall; //Tenias --> state.Agent //dar refuerzo positivo
                    break;

                case Status.Wall: // De pared a pared
                    Death(-3);
                    break;
            }
        }
        else if (collision.gameObject.name == "Agent") {
            switch (state) {
                case Status.Service:
                    float reward = goodReward;
                    reward -= count;
                    if (reward <= 0) reward = 1;

                    WellDone(goodReward);
                    count = 0;
                    state = Status.Agent;
                    break;

                case Status.Floor: // Le da tras un bote
                    /* if (firstTouch) {
                         m_Agent.AddReward(2); //esto pq amor?! sigues haciendo q bote en el saque la pelota!!!
                         firstTouch = false;
                     } else if (!firstTouch) {
                         m_Agent.AddReward(1);
                     }*/
                    WellDone(2);
                    state = Status.Agent;
                    break;

                case Status.Agent: // Doble hit
                    Death(-1f);
                    break;

                case Status.Wall: // Hit directo, ha de esperar a que toque el suelo
                    Death(-1f);
                    break;
            }

        }

        

    }

    public void Death(float negativeReward) {
        /*if (state == Status.Service) {
            if (currentLoses >= checkLoses) m_Agent.AddReward(-4); //currentReward -= 3;
            else if (currentLoses < checkLoses) m_Agent.AddReward(-1); //currentReward--; // Extra if no touch
        }*/
        //m_Agent.AddReward(currentReward - 1);
        m_Agent.AddReward(negativeReward);
        Reset();
    }
    public void WellDone(float positiveReward)
    {
        m_Agent.AddReward(positiveReward);
    }
    public bool CheckLostTooMuchGames(float lostGames)
    {
        if (currentLoses >= lostGames) return true;
        else return false;
    }
}
