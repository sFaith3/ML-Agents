using UnityEngine;

public class FoodLogic : MonoBehaviour
{
    public bool respawn;
    public FoodCollectorArea myArea;
    

    public void OnEaten(bool agentSatiated)
    {
        if(!agentSatiated)
        {
            if (respawn)
            {
                transform.position = new Vector3(Random.Range(-myArea.range, myArea.range),
                    3f,
                    Random.Range(-myArea.range, myArea.range)) + myArea.transform.position;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
}
