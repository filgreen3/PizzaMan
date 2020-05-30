﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonFabric : MonoBehaviour {

    public Person PersonPrefab;

    public Person CreatePerson (Vector2 position) {
        var person = Instantiate (PersonPrefab, position, Quaternion.identity);
        person.Dir = Vector2.right * -Mathf.Sign (position.x);
        person.Init (null);

        return person;
    }

}