using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCube : MonoBehaviour
{

    void Update()
    {
        transform.eulerAngles += new Vector3(0, 1, 0);
    }
}
