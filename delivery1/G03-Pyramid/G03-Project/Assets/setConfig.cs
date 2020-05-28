using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setConfig : MonoBehaviour
{

    //public GameObject agent;
    PushAgentBasic refPA;
    // Start is called before the first frame update
    void Awake()
    {
        refPA = transform.Find("Agent").gameObject.GetComponent<PushAgentBasic>();
        refPA.blockPurple = transform.Find("BlockPurple").gameObject;
        refPA.blockGreen = transform.Find("BlockGreen").gameObject;
        refPA.goalPurple = transform.Find("GoalPurple").gameObject;
        refPA.goalGreen = transform.Find("GoalGreen").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
