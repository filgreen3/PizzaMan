using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarForegroung : MonoBehaviour
{

    bool waiting = true;
    float t = 0;
    int waitfor = 1000;
    void Update()
    {
        if (!waiting)
        {
            transform.position += new Vector3(0.3f, Mathf.Sin(t / 6) / 16);
            t++;
            if (transform.position.x > 30)
            {
                //GetComponent<AudioSource>().Stop();
                t = 0;
                waiting = true;
            }
        }
    }
    private void FixedUpdate()
  
    {
        {
            t++;
            if (t > waitfor)
            {
                transform.position = new Vector2(-30, -3.2f);
                t = 0;
                waiting = false;
                GetComponent<AudioSource>().Play();
                waitfor = Random.Range(1000, 10000);
            }
        }

    }
}
