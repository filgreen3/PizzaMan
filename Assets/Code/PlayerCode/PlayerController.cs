using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;

    public Vector2 RectMain;
    public Vector2 RectExtra;
    public Vector2 Offset;

    [SerializeField] private SpriteRenderer spriteRenderer;

    public Transform transf;

    private float axisX, axisY;

    void FixedUpdate () {

        axisX = Input.GetAxis ("Horizontal");
        axisY = Input.GetAxis ("Vertical");

        if ((transf.position.y < RectMain.y / 2f + Offset.y || axisY < 0) &&
            (transf.position.y > -RectMain.y / 2f + Offset.y || axisY > 0))
            transf.Translate (0, axisY * speed, 0);

        if ((transf.position.x < RectMain.x / 2f + Offset.x || axisX < 0) &&
            (transf.position.x > -RectMain.x / 2f + Offset.x || axisX > 0))
            transf.Translate (axisX * speed, 0, 0);

        if (Mathf.Abs (transf.position.x) > RectExtra.x / 2f) {
            spriteRenderer.color = Color.red;
        } else {
            spriteRenderer.color = Color.white;
        }
    }

    void OnDrawGizmos () {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube (Offset, RectMain);

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube (Offset, RectExtra);
    }

}