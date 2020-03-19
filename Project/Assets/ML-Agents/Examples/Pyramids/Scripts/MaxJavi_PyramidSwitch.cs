using UnityEngine;


public class MaxJavi_PyramidSwitch : MonoBehaviour
{
    public Material onMaterial;
    public Material offMaterial;
    public GameObject myButton;
    bool m_State;
    GameObject m_Area;
    MaxJavi_PyramidArea m_AreaComponent;
    int m_PyramidIndex;
    public enum colorButtonEnum { RED, ORANGE };
    public colorButtonEnum buttonColor;
    public GameObject agent;
    MaxJavi_PyramidAgent pyramidAgentScript;

    public bool GetState()
    {
        return m_State;
    }

    void Start()
    {
        m_Area = gameObject.transform.parent.gameObject;
        m_AreaComponent = m_Area.GetComponent<MaxJavi_PyramidArea>();
        agent.GetComponent<GameObject>();
        pyramidAgentScript = agent.GetComponent<MaxJavi_PyramidAgent>();

    }

    public void ResetSwitch(int spawnAreaIndex, int pyramidSpawnIndex)
    {
        m_AreaComponent.PlaceObject(gameObject, spawnAreaIndex);
        m_State = false;
        m_PyramidIndex = pyramidSpawnIndex;

        if (gameObject.name == "RedSwitch")
        {
            tag = "switchOff_R";
        }
        else
        {
            tag = "switchOff_O";
        }
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        myButton.GetComponent<Renderer>().material = offMaterial;
        pyramidAgentScript.buttonCounter = 0;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("agent") && m_State == false)
        {

            myButton.GetComponent<Renderer>().material = onMaterial;
            m_State = true;
            if (gameObject.name == "RedSwitch")
            {
                tag = "switchOn_R";
            }
            else
            {
                tag = "switchOn_O";
            }
            pyramidAgentScript.buttonCounter += 1;
            
        }
      
    }
    private void Update()
    {
        if(pyramidAgentScript.buttonCounter == 2)
        {
            m_AreaComponent.CreatePyramid(1, m_PyramidIndex);
            pyramidAgentScript.buttonCounter = 0;
        }
    }
}
