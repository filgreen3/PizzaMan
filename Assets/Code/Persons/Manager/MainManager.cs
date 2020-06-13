using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour {

    public float TimeDelay;
    [Range (0f, 30f)] public float SizeX;
    public Vector3 Offset;
    [Range (0.0177f, 0.5f)] public float Speed;

    [SerializeField] private PersonFabric fabric;

    private float currentTimeDelay;
    private PositionStruction struction;

    [Range (-3f, 3f)] public float mainLine;
    [Range (0f, 3f)] public float delta;

    private void Start () {
        for (int i = 0; i < 15; i++) {

            var point = Vector3.right * SizeX * Mathf.Sign (Random.value - 0.5f);

            var person = fabric.CreatePerson (point);
            person.Speed = Speed;
            person.mainLine = mainLine;
            person.delta = delta;
            person.Init ();

            var y = ((Random.value - 0.5f) * delta + mainLine);

            person.transform.position = Vector3.right * Random.Range (-SizeX * .5f, SizeX * .5f) +
                Vector3.up * y + Vector3.forward * y;
        }
    }

    void OnDrawGizmos () {

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube (Vector3.up * mainLine, Vector3.right * SizeX + Vector3.up * delta);
        Gizmos.color = Color.green - Color.black * 0.8f;
        Gizmos.DrawCube (Vector3.up * mainLine, Vector3.right * SizeX + Vector3.up * delta);

    }
}