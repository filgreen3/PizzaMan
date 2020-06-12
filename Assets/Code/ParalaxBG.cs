using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxBG : MonoBehaviour
{
    public float k = 20;
    Vector3 start;
    Transform cam;
    void Start()
    {
        start = transform.position;
        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = start + cam.position/k;
    }
}
