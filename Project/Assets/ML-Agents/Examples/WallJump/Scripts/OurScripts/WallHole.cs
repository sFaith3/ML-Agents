using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHole : MonoBehaviour
{
    public float PercentageHole;

    private float _percentageLeftCube;
    private float _percentageRightCube;

    public GameObject CubeLeft;
    public GameObject CubeRight;
    public GameObject CubeTop;
    public GameObject CubeBottom;
    public GameObject Hole;

    // Start is called before the first frame update
    void Start()
    {
        ResetHole();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    ResetHole();
        //}

        //if (Input.GetKeyDown(KeyCode.G))
        //{
        //    var localScale = transform.localScale;
        //    localScale = new Vector3(localScale.x, 0f, localScale.z);
        //    transform.localScale = localScale;
        //}

        //if (Input.GetKeyDown(KeyCode.H))
        //{
        //    var localScale = transform.localScale;
        //    localScale = new Vector3(localScale.x, 0.9222f, localScale.z);
        //    transform.localScale = localScale;
        //}

        //if (Input.GetKeyDown(KeyCode.J))
        //{
        //    var localScale = transform.localScale;
        //    localScale = new Vector3(localScale.x, 1.5f, localScale.z);
        //    transform.localScale = localScale;
        //}

        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    var localScale = transform.localScale;
        //    localScale = new Vector3(localScale.x, 3.5f, localScale.z);
        //    transform.localScale = localScale;
        //}
    }

    public void ResetHole()
    {
        _percentageLeftCube = Random.Range(0.0f, 1.0f - PercentageHole);
        _percentageRightCube = 1.0f - PercentageHole - _percentageLeftCube;

        CubeLeft.transform.localScale = new Vector3(_percentageLeftCube, CubeLeft.transform.localScale.y, CubeLeft.transform.localScale.z);
        CubeLeft.transform.localPosition = new Vector3(-0.5f + (_percentageLeftCube / 2), CubeLeft.transform.localPosition.y, CubeLeft.transform.localPosition.z);


        CubeRight.transform.localScale = new Vector3(_percentageRightCube, CubeRight.transform.localScale.y, CubeRight.transform.localScale.z);
        CubeRight.transform.localPosition = new Vector3(0.5f - (_percentageRightCube / 2), CubeRight.transform.localPosition.y, CubeRight.transform.localPosition.z);

        if (Hole)
        {
            Hole.transform.localScale = new Vector3(PercentageHole, Hole.transform.localScale.y, Hole.transform.localScale.z);
            Hole.transform.position = new Vector3(CubeLeft.GetComponent<MeshRenderer>().bounds.center.x + CubeLeft.GetComponent<MeshRenderer>().bounds.size.x / 2 + Hole.GetComponent<MeshRenderer>().bounds.size.x / 2, Hole.transform.position.y, Hole.transform.position.z);
        }
    }
}
