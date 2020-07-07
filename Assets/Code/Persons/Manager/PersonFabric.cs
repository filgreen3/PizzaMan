using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonFabric : MonoBehaviour
{
    public Person[] PersonPrefab;
    public Person CreatePerson(Vector3 position, int id)
    {
        var person = Instantiate(PersonPrefab[id], position, Quaternion.identity);
        person.Dir = Vector2.right * -Mathf.Sign(Random.value - 0.5f);
        return person;
    }
}