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

    // Start is called before the first frame update
    void Start()
    {
        _percentageLeftCube = Random.Range(0.0f, 1.0f - PercentageHole);
        _percentageRightCube = 1.0f - PercentageHole - _percentageLeftCube;

        CubeLeft.transform.localScale = new Vector3(_percentageLeftCube, CubeLeft.transform.localScale.y, CubeLeft.transform.localScale.z);
        CubeLeft.transform.localPosition = new Vector3(- 0.5f +(_percentageLeftCube / 2), CubeLeft.transform.localPosition.y, CubeLeft.transform.localPosition.z);


        CubeRight.transform.localScale = new Vector3(_percentageRightCube, CubeRight.transform.localScale.y, CubeRight.transform.localScale.z);
        CubeRight.transform.localPosition = new Vector3(0.5f - (_percentageRightCube / 2), CubeRight.transform.localPosition.y, CubeRight.transform.localPosition.z);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            _percentageLeftCube = Random.Range(0.0f, 1.0f - PercentageHole);
            _percentageRightCube = 1.0f - PercentageHole - _percentageLeftCube;

            CubeLeft.transform.localScale = new Vector3(_percentageLeftCube, CubeLeft.transform.localScale.y, CubeLeft.transform.localScale.z);
            CubeLeft.transform.localPosition = new Vector3(-0.5f + (_percentageLeftCube / 2), CubeLeft.transform.localPosition.y, CubeLeft.transform.localPosition.z);


            CubeRight.transform.localScale = new Vector3(_percentageRightCube, CubeRight.transform.localScale.y, CubeRight.transform.localScale.z);
            CubeRight.transform.localPosition = new Vector3(0.5f - (_percentageRightCube / 2), CubeRight.transform.localPosition.y, CubeRight.transform.localPosition.z);
        }
    }
}
