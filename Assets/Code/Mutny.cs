using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mutny : MonoBehaviour
{
    bool activated = false;
    public GameObject Enable;
    public float speed = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player"&&!activated) { 
            activated = true;
            Enable.SetActive(true);
        }
    }
    void Update()
    {
        transform.position += Vector3.left * speed*Time.deltaTime / 2;
    }
}
