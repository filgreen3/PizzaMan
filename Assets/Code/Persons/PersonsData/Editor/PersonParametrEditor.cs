using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor (typeof (PersonParametr), true)]
[CanEditMultipleObjects]
public class PersonParametrEditor : Editor {

    PersonParametr parametr;

    [MyEditor.RequireInterface (typeof (DataEntityGiver))]
    List<Object> personParametrs = new List<Object> (5);

    private void OnEnable () {
        parametr = ((PersonParametr) target);
        foreach (var item in parametr.datas) {
            personParametrs.Add ((Object) item);
        }
    }
}