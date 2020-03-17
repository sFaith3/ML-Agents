using UnityEngine;

public class HitWall_Keep : MonoBehaviour
{
    public GameObject areaObject;
    public int lastAgentHit;
    public bool net;

    public enum Status
        {
            Service,
            Floor,
            Agent,
            Wall
        }

    public Status state;

    TennisKeepArea m_Area;
    TennisKeepAgent m_Agent;

    int servicesFailed = 0;

    //  Use this for initialization
    void Start()
    {
        m_Area = areaObject.GetComponent<TennisKeepArea>();
        m_Agent = m_Area.agent.GetComponent<TennisKeepAgent>();
        //firstTouch = true;
        state = Status.Service;
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

        state = Status.Service;
    }

    void OnCollisionEnter(Collision collision)
    { 
        if (collision.gameObject.name == "Floor") {
            switch(state)
            {
                case Status.Service:
                    m_Agent.AddReward(-4);
                    Reset();
                    break;
                case Status.Floor:
                    m_Agent.AddReward(-1);
                    Reset();
                    break;
                case Status.Agent:
                    m_Agent.AddReward(-1);
                    Reset();
                    break;
                case Status.Wall:
                    state = Status.Floor;
                    break;
            }
            
        }
        else if (collision.gameObject.tag == "iWall")
        {
            switch (state)
            {
                case Status.Service: // No deberia ocurrir pero por si acaso
                    m_Agent.AddReward(-3);
                    break;

                case Status.Floor: // De floor a wall
                    m_Agent.AddReward(-1);
                    break;

                case Status.Agent:
                    m_Agent.AddReward(2);
                    state = Status.Wall;
                    break;

                case Status.Wall: // De pared a pared
                    m_Agent.AddReward(-3);
                    break;
            }
        }
        else if (collision.gameObject.name == "Agent")
        {
            switch (state)
            {
                case Status.Service: // Si se está de saque y el agente le da
                    //float tmpReward = 5;
                    //if (servicesFailed > 5 && currentLoses < 9000) tmpReward += goodReward;
                    m_Agent.AddReward(5);
                    servicesFailed = 0; // reset de los fallados
                    state = Status.Agent;
                    break;

                case Status.Floor: // Le da tras un bote
                    m_Agent.AddReward(2);
                    state = Status.Agent;
                    break;

                case Status.Agent: // Doble hit
                    m_Agent.AddReward(-1f);
                    break;

                case Status.Wall: // Hit directo, ha de esperar a que toque el suelo
                    m_Agent.AddReward(-1f);
                    break;
            }
        }


        //if (collision.gameObject.tag == "iWall")
        //{
        //    m_Agent.AddReward(-0.5f);
        //    Reset();
        //}

        
        //if(collision.gameObject.tag == "Agent")
        //{
        //    m_Agent.AddReward(2);
        //}

    }
}
