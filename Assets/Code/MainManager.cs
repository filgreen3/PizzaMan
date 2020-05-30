using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour {

    public float TimeDelay;
    [Range (0f, 1f)] public float Size;
    [Range (0.0177f, 0.05f)] public float Speed;

    [SerializeField] private PersonFabric fabric;

    private float currentTimeDelay;
    private PositionStruction struction;

    private Vector2[] line1;
    private Vector2[] line2;

    private void Start () {
        line1 = new Vector2[] {
            -Vector2.right * 10 * Size,
            Vector2.up - Vector2.right * 10 * Size,
            2 * Vector2.up - Vector2.right * 10 * Size
        };
        line2 = new Vector2[] {
            Vector2.right * 10 * Size,
            Vector2.up + Vector2.right * 10 * Size,
            2 * Vector2.up + Vector2.right * 10 * Size
        };

        struction = new PositionStruction (line1, line2);
        StartCoroutine (MainLoop ());
    }

    IEnumerator MainLoop () {
        currentTimeDelay = TimeDelay;

        while (true) {
            fabric.CreatePerson (struction.GetPosition).Speed = Speed;
            yield return new WaitForSeconds (currentTimeDelay);
        }
    }

    void OnDrawGizmos () {
        Gizmos.color = Color.green;

        if (line1 != null && line2 != null) {
            for (int i = 0; i < line1.Length; i++) {
                Gizmos.DrawLine (line1[i], line2[i]);
                Gizmos.DrawWireSphere (line1[i], 0.5f);
                Gizmos.DrawWireSphere (line2[i], 0.5f);
            }
        }
    }
}