using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonFabric : MonoBehaviour {

    public Person PersonPrefab;
    public PersonPreset Preset;

    private void Awake () {
        Preset.SortParametrs ();
    }

    public Person CreatePerson (Vector3 position) {
        var person = Instantiate (PersonPrefab, position, Quaternion.identity);
        person.Dir = Vector2.right * -Mathf.Sign (position.x);
        person.Init (Preset.GetDataPasport ());
        return person;
    }

}