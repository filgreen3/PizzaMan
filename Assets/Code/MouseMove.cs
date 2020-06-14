using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMove : MonoBehaviour {
    [SerializeField] private float radious;
    [SerializeField] private Vector3 offset;
    Animator animator;
    private Transform transf;
    private Camera cameraMain;

    private void Awake () {
        transf = transform;
        cameraMain = Camera.main;
        animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

    private void FixedUpdate () {
        var point = cameraMain.ScreenToWorldPoint (Input.mousePosition) - transf.parent.position;
        point.z = 0;

        var diff = point.normalized;
        float rotZ = Mathf.Atan2 (diff.y, diff.x) * Mathf.Rad2Deg;
        transf.rotation = Quaternion.Euler (0f, 0f, rotZ - 90);

        point = Vector3.ClampMagnitude (point, radious);
        point = point * radious + offset;
        transf.localPosition = point;

    }
    private void Update()
    {
        if (Time.timeScale > 0)
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
                if (transf.localPosition.y > 5) animator.SetTrigger("Give_up");
                else animator.SetTrigger("Give_down");
    }
    void OnDrawGizmos () {
        if (transf) {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere (transf.position, 2f);
        }
    }
}