using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonFabric : MonoBehaviour {

    public Person PersonPrefab;
    public PersonPreset Preset;

    public Person CreatePerson (Vector3 position) {
        var person = Instantiate (PersonPrefab, position, Quaternion.identity);
        person.Dir = Vector2.right * -Mathf.Sign (position.x);
        person.Init (Preset);

        return person;
    }

}