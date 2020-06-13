using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cloud : MonoBehaviour
{
    SpriteRenderer sr;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        transform.position += Vector3.right*Time.deltaTime/2;
        if (transform.position.x>24)
        {

            transform.position = new Vector2(-24,Random.Range(3.0f,5.0f));
            float scale = Random.Range(0.5f, 0.7f);
            transform.localScale = new Vector2(scale, scale);
            if (transform.position.y > 3) sr.sortingOrder = -11;
            if (transform.position.y > 3.7f) sr.sortingOrder = -12;
            if (transform.position.y > 4.4f) sr.sortingOrder = -13;

        }
    }
}
