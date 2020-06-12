using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScenario : MonoBehaviour
{
    public GameObject Dialogue1; 
    void Start()
    {
        StartCoroutine("StartDialogue");
    }

IEnumerator StartDialogue()
    {
        yield return new WaitForSeconds(1);
        Dialogue1.SetActive(true);
    }
}
