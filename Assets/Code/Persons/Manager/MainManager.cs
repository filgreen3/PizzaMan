using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour {

    public float TimeDelay;
    [Range (0f, 10f)] public float SizeX, SizeY;
    public Vector3 Offset;
    [Range (0.0177f, 0.5f)] public float Speed;

    [SerializeField] private PersonFabric fabric;

    private float currentTimeDelay;
    private PositionStruction struction;

    private Vector3[] line1;
    private Vector3[] line2;

    private void Start () {
        line1 = new Vector3[] {
            -Vector3.right * 10 * SizeX + Offset,
            Vector3.up * SizeY - Vector3.right * 10 * SizeX + Vector3.forward + Offset,
            2 * Vector3.up * SizeY - Vector3.right * 10 * SizeX + Vector3.forward * 2 + Offset
        };
        line2 = new Vector3[] {
            Vector3.right * 10 * SizeX + Offset,
            Vector3.up * SizeY + Vector3.right * 10 * SizeX + Vector3.forward + Offset,
            2 * Vector3.up * SizeY + Vector3.right * 10 * SizeX + Vector3.forward * 2 + Offset
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