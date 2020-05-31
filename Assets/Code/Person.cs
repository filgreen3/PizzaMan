using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour {

    public PersonData Data;

    [HideInInspector] public float Speed;
    [HideInInspector] public Vector3 Dir;

    private SpriteRenderer spriteRenderer;
    private Transform transf;

    private Vector3 startPosition;

    public void Init (PersonData data) {
        startPosition = transf.position;
        startPosition.x = Mathf.Abs (startPosition.x);

        //        for (int i = 0; i < data.Parametrs.Length; i++) {
        //            Data = data;
        //        }

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