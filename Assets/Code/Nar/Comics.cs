using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Comics : MonoBehaviour
{

    float t=0;
    public float[] dur;
    int n = 0;
    public SpriteRenderer[] sprite;

    void Start()
    {
        Time.timeScale = 0;
        for (int i = 0; i < sprite.Length; i++)
        {
            sprite[i].color = new Color(1, 1, 1, 0);

        }




    }

    void Update()
    {
        if (n >= sprite.Length)
        {
            if (Input.anyKey) Close();

        }
        else
        for (int i = 0; i < sprite.Length; i++)
            if (n < sprite.Length&&t < dur[n])
            {
                sprite[n].color = new Color(1, 1, 1, t);
                t += Time.fixedUnscaledDeltaTime/4;
            }
            else 
            { 
                n++;
                t = 0;

            }
    }
    void Close()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
