using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PersonElement {
    [HideInInspector] public string ItemName;
    public bool Active;

    public PersonElement (string itemName, bool active) {
        ItemName = itemName;
        Active = active;
    }

}
