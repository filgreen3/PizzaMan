using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuferLevel : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Next;
    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey) Next.SetActive(true);
    }
}
