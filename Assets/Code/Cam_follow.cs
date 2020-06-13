using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam_follow : MonoBehaviour
{
    public Transform GG;
    public float offset;
    void Update()
    {
        if (GG.position.x + offset <= 9.5f && GG.position.x + offset >= -9.5f)
            transform.position = Vector3.Lerp(transform.position, new Vector3(GG.position.x+ offset, 0, -10), Time.deltaTime);
        else
        { 
            if (GG.position.x+ offset < 0) transform.position = Vector3.Lerp(transform.position, new Vector3(-9.5f, 0, -10), Time.deltaTime);
            else transform.position = Vector3.Lerp(transform.position, new Vector3(9.5f, 0, -10), Time.deltaTime);
        }
    }
}
