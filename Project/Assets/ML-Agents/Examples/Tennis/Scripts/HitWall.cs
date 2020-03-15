using UnityEngine;

public class HitWall : MonoBehaviour
{
    public GameObject areaObject;
    public int lastAgentHit;
    public int currentLoses;

    private float checkLostGames = 1500f;
    private float servicesFailed = 0; // Check de cuantas primeras bolas seguidas lleva falladas para un castigo incrementado
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
        currentLoses = 0;
        state = Status.Service;
        servicesFailed = 0;
    }

    void Reset()
    {
        m_Agent.Done();
        m_Area.MatchReset();
        currentLoses++;
        state = Status.Service;
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.name == "Floor") { 
            switch (state) {
                case Status.Service: // Falla saque
                    servicesFailed++;
                    Death(-servicesFailed * 4);
                    break;

                case Status.Floor: // Doble bote en el suelo
                    Death(-1);
                    break;

                case Status.Agent: // De raqueta a suelo
                    Death(-1);
                    break;

                case Status.Wall: // De pared a suelo
                    state = Status.Floor;
                    break;
            }
        }
        else if (collision.gameObject.name == "WallFront") {
            switch (state) {
                case Status.Service: // No deberia ocurrir pero por si acaso
                    Death(-3);
                    break;

                case Status.Floor: // De floor a wall
                    Death(-1);
                    break;

                case Status.Agent:
                    GivePositiveReward(2);
                    state = Status.Wall;
                    break;

                case Status.Wall: // De pared a pared
                    Death(-3);
                    break;
            }
        }
        else if (collision.gameObject.name == "Agent") {
            switch (state) {
                case Status.Service: // Si se está de saque y el agente le da
                    float tmpReward = goodReward;
                    if (servicesFailed > 5 && currentLoses < 9000) tmpReward += goodReward;
                    GivePositiveReward(tmpReward);
                    servicesFailed = 0; // reset de los fallados
                    state = Status.Agent;
                    break;

                case Status.Floor: // Le da tras un bote
                    GivePositiveReward(2);
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
        if (currentLoses > 9000) negativeReward *= 2;
        m_Agent.AddReward(negativeReward);
        Reset();
    }
    public void GivePositiveReward(float positiveReward) {
        m_Agent.AddReward(positiveReward);
    }
    public bool CheckLostTooMuchGames(float lostGames) {
        if (currentLoses >= lostGames) return true;
        else return false;
    }
}
