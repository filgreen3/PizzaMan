using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTutorial : MonoBehaviour
{
    int i = 0;
    int t = 0;
    public GameObject[] BossReply;

    void Next()
    {
        BossReply[i].SetActive(false);
        i++;
        BossReply[i].SetActive(true);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)|| Input.GetKeyDown(KeyCode.D) && i == 0&& BossReply[i].activeSelf)
        {
            Next();
        }
        if (Input.GetMouseButtonDown(0) && i == 1)
        {
            Next();
        }

        if (t<500 && i == 2)
        {
            t++;
        }
        if (t>=500 && i == 2)
        {
            BossReply[i].SetActive(false);
            i++;
        }

    }
}
