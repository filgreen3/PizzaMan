using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor (typeof (PersonData))]
public class EditorPersonData : Editor {
    private List<string> namesList = new List<string> ();
    private bool[] namesListShow;
    private PersonData data;
    void OnEnable () {
        data = (PersonData) target;
        foreach (var item in data.Elements) {
            if (item.ItemName.LastIndexOf ("_") > 0) {
                var name = item.ItemName.Remove (item.ItemName.LastIndexOf ("_"));
                if (!namesList.Contains (name)) {
                    namesList.Add (name);
                }
            }
        }
        for (int i = namesList.Count - 1; i >= 0; i--) {
            if (namesList[i].LastIndexOf ("_") > 0) {
                namesList.RemoveAt (i);
            }
        }
        namesListShow = new bool[namesList.Count];
    }
    public override void OnInspectorGUI () {
        EditorGUILayout.PropertyField (serializedObject.FindProperty ("PersonTransform"));
        for (int i = 0; i < namesList.Count; i++) {
            if (namesListShow[i] = EditorGUILayout.Foldout (namesListShow[i], namesList[i])) {
                foreach (var item in data.Elements) {
                    if (item.ItemName.StartsWith (namesList[i]))
                        item.Active = EditorGUILayout.Toggle (item.ItemName, item.Active);
                }
            }
        }
    }
}