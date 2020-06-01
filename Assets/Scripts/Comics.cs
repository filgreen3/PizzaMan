using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comics : MonoBehaviour
{

    float t=0;
    public float[] dur;
    int n = 0;
    public SpriteRenderer[] sprite;

    void Start()
    {

        for (int i = 0; i < sprite.Length; i++)
        {
            sprite[i].color = new Color(1, 1, 1, 0);

        }

    }

    void FixedUpdate()
    {
        for (int i = 0; i < sprite.Length; i++)
            if (t < dur[n])
            {
                sprite[n].color = new Color(1, 1, 1, t);
                t += 0.01f;
            }
            else 
            { 
                n++;
                t = 0;
                if (n == sprite.Length) gameObject.GetComponent<Comics>().enabled = false;
            }
    }
    
}
