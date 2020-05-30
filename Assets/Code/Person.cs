using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour {

    [HideInInspector] public float Speed;
    [HideInInspector] public Vector2 Dir;

    private SpriteRenderer spriteRenderer;
    private Transform transf;

    private Vector2 startPosition;

    public void Init (PersonData data) {
        startPosition = transf.position;
        startPosition.x = Mathf.Abs (startPosition.x);
    }

    private void Awake () {
        transf = transform;
        spriteRenderer = GetComponent<SpriteRenderer> ();
    }

    private void FixedUpdate () {
        transf.Translate (Dir * Speed);
        if (Mathf.Abs (transf.position.x) > startPosition.x) {
            Destroy (gameObject);
        }
    }
}