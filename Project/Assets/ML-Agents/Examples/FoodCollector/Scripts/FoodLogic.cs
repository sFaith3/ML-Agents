using UnityEngine;

public class FoodLogic : MonoBehaviour
{
    public bool isEaten;
    public bool respawn;
    public FoodCollectorArea myArea;

    public void Respawn()
    {
        /*Rigidbody rc = gameObject.AddComponent<Rigidbody>() as Rigidbody;
        GetComponent<Rigidbody>().mass = 5f;*/
        transform.position = new Vector3(Random.Range(-myArea.range, myArea.range),
                    3f,
                    Random.Range(-myArea.range, myArea.range)) + myArea.transform.position;
    }
}
