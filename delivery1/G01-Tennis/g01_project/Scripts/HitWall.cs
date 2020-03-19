using UnityEngine;

public class HitWall : MonoBehaviour
{
    public GameObject areaObject;
    public int lastAgentHit;
    public int currentLoses; //Variable de control de output de puntos fallados
    public int currentTouches; //Variable de control de output de puntos buenos

    private bool firstGame; //Variable de control si es saque.

    private enum Status { //Enum de control de estado de la pelota. Que ha tocado previamente a la colision
        Floor, Agent, Wall
    };
    Status state;

    TennisArea m_Area;
    TennisAgent m_Agent;

    void Start()
    {
        m_Area = areaObject.GetComponent<TennisArea>();
        m_Agent = m_Area.agent.GetComponent<TennisAgent>();
        currentLoses = 0;
        currentTouches = 0;
        state = Status.Floor;
        firstGame = true;
    }

    void Reset()
    {
        m_Agent.Done();
        m_Area.MatchReset();
        currentLoses++;
        state = Status.Floor;
        firstGame = true;
    }

    void OnCollisionEnter(Collision collision) {
        switch (state) {
            case Status.Floor:
                if (collision.gameObject.name == "Agent") {
                    state = Status.Agent;
                } 
                else Death();
                break;

            case Status.Agent:
                if (collision.gameObject.name == "WallFront") {
                    if (!firstGame) {
                        currentTouches++;
                        GivePositiveReward();
                    }
                    else {
                        firstGame = false;
                        GivePositiveReward_Less();
                    }
                    state = Status.Wall;
                }
                else Death();
                break;

            case Status.Wall:
                if (collision.gameObject.name == "Floor") state = Status.Floor;
                else Death();
                break;
        }
    }

    public void Death() {       
        m_Agent.AddReward(-1); 
        Reset();
    }
    public void Death(float reward) {       
        m_Agent.AddReward(reward); 
        Reset();
    }
    public void GivePositiveReward() {
        m_Agent.AddReward(1); //como maxSteps es 50M, no llegaremos a 25M asi que lo dejamos en +1
       //m_Agent.AddReward(Mathf.Min(2 * Mathf.Abs(1f - ((float)m_Agent.GetStepCount() / (float)m_Agent.maxStep)), 1f)); //Hasta que no llegue a la mitad de los maxSteps, conseguira un reward de +1. Luego, dicho reward irá bajando de forma lineal.
    }
    public void GivePositiveReward_Less() {
        m_Agent.AddReward(0.5f); //Lo mismo que en GivePositiveReward
        //m_Agent.AddReward((Mathf.Min(2 * Mathf.Abs(1f - ((float)m_Agent.GetStepCount() / (float)m_Agent.maxStep)), 1f)/10)*5); //Lo mismo pero en vez de +1, consigue +0.5f
    }
}
