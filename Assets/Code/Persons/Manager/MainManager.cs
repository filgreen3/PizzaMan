using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour {

    public int PeopleCount;

    [Range (0f, 60f)] public float SizeX;
    [Range (0.0177f, 0.5f)] public float Speed;

    [Range (-10f, 10f)] public float mainLine;
    [Range (0f, 3f)] public float delta;

    [SerializeField] private PersonFabric fabric;
    [SerializeField] private PersonMatchManager matchManager;

    private void Start () {
        var persons = new Person[PeopleCount];
        for (int i = 0; i < PeopleCount; i++) {

            var point = Vector3.right * SizeX * 0.5f * Mathf.Sign (Random.value - 0.5f);

            var person = fabric.CreatePerson (point);
            person.Speed = Speed;
            person.mainLine = mainLine;
            person.delta = delta;
            person.Init ();

            var y = ((Random.value - 0.5f) * delta + mainLine);

            person.transform.position = Vector3.right * SizeX * (Random.value - 0.5f) +
                Vector3.up * y + Vector3.forward * y*10;

            persons[i] = person;

        }
        matchManager.PersonTransforms = persons;
    }

    void OnDrawGizmos () {

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube (Vector3.up * mainLine, Vector3.right * SizeX + Vector3.up * delta);
        Gizmos.color = Color.green - Color.black * 0.8f;
        Gizmos.DrawCube (Vector3.up * mainLine, Vector3.right * SizeX + Vector3.up * delta);

    }
}