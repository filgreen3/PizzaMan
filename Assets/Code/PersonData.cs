using UnityEngine;

[CreateAssetMenu (fileName = "Data", menuName = "ScriptableObjects/PersonData", order = 1)]
public class PersonData : ScriptableObject {
    public PersonParametr[] Parametrs;

    public bool Match (PersonData person) {

        if (person.Parametrs.Length != Parametrs.Length) return false;

        for (int i = 0; i < Parametrs.Length; i++) {
            if (Parametrs[i].CurrentData != person.Parametrs[i].CurrentData)
                return false;
        }
        return true;
    }
}
