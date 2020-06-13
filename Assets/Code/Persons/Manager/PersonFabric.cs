using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonFabric : MonoBehaviour {

    public Person[] PersonPrefab;
    public PersonPreset[] Preset;

    private void Awake () {
        foreach (var item in Preset) {
            item.SortParametrs ();
        }
    }

    public Person CreatePerson (Vector3 position) {
        var id = Random.Range (0, PersonPrefab.Length);
        var person = Instantiate (PersonPrefab[id], position, Quaternion.identity);
        person.Dir = Vector2.right * -Mathf.Sign (Random.value - 0.5f);
        person.preset = Preset[id];
        return person;
    }
}