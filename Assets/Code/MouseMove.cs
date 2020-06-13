using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMove : MonoBehaviour {
    [SerializeField] private float radious;
    [SerializeField] private Vector3 offset;

    private Transform transf;
    private Camera cameraMain;

    private void Awake () {
        transf = transform;
        cameraMain = Camera.main;

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

    void OnDrawGizmos () {
        if (transf) {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere (transf.position, 1f);
        }
    }
}