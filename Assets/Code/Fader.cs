using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fader : MonoBehaviour
{
    bool faded = false;
    Image Image;
    float t = 1;
    public int next;
    private void Start()
    {
        Image = GetComponent<Image>();
    }
    void Update()
    {
        if (!faded) 
        {
            Image.color = new Color(0, 0, 0, t);
            t-=0.02f;
            if (t<=0)
            {
                t = 0;
                faded = true;
                gameObject.SetActive(false);
            }

        }
        else        
        {
            Image.color = new Color(0, 0, 0, t);
            t+=0.02f;
            if (t>=1)
            {
                Time.timeScale = 1;
                if (next!=0)PlayerPrefs.SetInt("Level", next);
                SceneManager.LoadScene(next);
            }


        }

    }
}
