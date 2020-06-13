using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScenario : MonoBehaviour
{
    public GameObject Dialogue1; 
    public int time=1; 
    void Start()
    {
        StartCoroutine("StartDialogue");
    }

IEnumerator StartDialogue()
    {
        yield return new WaitForSeconds(time);
        Dialogue1.SetActive(true);
    }
}
