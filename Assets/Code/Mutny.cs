using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mutny : MonoBehaviour
{
    bool activated = false;
    public GameObject Enable;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player"&&!activated) { 
            activated = true;
            Enable.SetActive(true);
        }
    }
    void Update()
    {
        transform.position += Vector3.left * Time.deltaTime / 2;
    }
}
