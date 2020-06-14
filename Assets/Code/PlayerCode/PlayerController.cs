using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    public GameObject BossReply0;
    public GameObject BossReply1;
    public GameObject BossReply2;
    public GameObject BossReply3;
    public GameObject Comix;
    public Vector2 RectMain;
    public Vector2 RectExtra1;
    public Vector2 RectExtra2;
    public Vector2 RectExtra3;
    public Vector2 RectExtra4;
    public Vector2 Offset;

    [SerializeField] private SpriteRenderer spriteRenderer;

    public Transform transf;
    Animator animator;
    private float axisX, axisY;

    private void Start () {
        animator = GetComponent<Animator> ();
    }
    void FixedUpdate () {

        axisX = Input.GetAxis ("Horizontal");
        axisY = Input.GetAxis ("Vertical");
        if (axisX != 0 || axisY != 0) animator.SetBool ("Run", true);
        else animator.SetBool ("Run", false);
        if (axisX < 0) spriteRenderer.flipX = true;
        if (axisX > 0) spriteRenderer.flipX = false;

        if ((transf.position.y < RectMain.y / 2f + Offset.y || axisY < 0) &&
            (transf.position.y > -RectMain.y / 2f + Offset.y || axisY > 0))
            transf.Translate (0, axisY * speed / 2, 0);

        if ((transf.position.x < RectMain.x / 2f + Offset.x || axisX < 0) &&
            (transf.position.x > -RectMain.x / 2f + Offset.x || axisX > 0))
            transf.Translate (axisX * speed, 0, 0);
        transf.position = new Vector3(transf.position.x, transf.position.y, transf.position.y);
        if (Mathf.Abs (transf.position.x) > RectExtra1.x / 2f && Mathf.Abs (transf.position.x) < RectExtra2.x / 2f) {
            BossReply0.SetActive (false);
            BossReply1.SetActive (true);
            BossReply2.SetActive (false);
            BossReply3.SetActive (false);
        }
        if (Mathf.Abs (transf.position.x) > RectExtra2.x / 2f && Mathf.Abs (transf.position.x) < RectExtra3.x / 2f) {

            BossReply2.SetActive (true);
            BossReply1.SetActive (false);
            BossReply3.SetActive (false);
        }
        if (Mathf.Abs (transf.position.x) > RectExtra3.x / 2f && Mathf.Abs (transf.position.x) < RectExtra4.x / 2f) {

            BossReply3.SetActive (true);
            BossReply2.SetActive (false);
            BossReply1.SetActive (false);
        }
        if (BossReply1 != null && Mathf.Abs (transf.position.x) > RectExtra4.x / 2f) {
            BossReply1.SetActive (false);
            BossReply2.SetActive (false);
            BossReply3.SetActive (false);
            Comix.SetActive (true);
        }
        if (BossReply1 != null && Mathf.Abs (transf.position.x) < RectExtra1.x / 2f) {
            BossReply1.SetActive (false);

        }
    }

    void OnDrawGizmos () {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube (Offset, RectMain);

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube (Offset, RectExtra1);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube (Offset, RectExtra2);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube (Offset, RectExtra3);
    }

    private void Update () {
        if (PersonMatchManager.instance == null) return;

        if (Input.GetMouseButtonDown (0))
            PersonMatchManager.Match (true);

        if (Input.GetMouseButtonDown (1))
            PersonMatchManager.Match (false);
    }

}