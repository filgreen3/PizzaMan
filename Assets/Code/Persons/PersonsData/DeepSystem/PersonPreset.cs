using UnityEngine;

[CreateAssetMenu (fileName = "Data", menuName = "ScriptableObjects/PersonData", order = 1)]
public class PersonPreset : ScriptableObject {
    public PersonParametr[] Parametrs;

    public bool Match (PersonPreset person) {

        foreach (var baseParametr in Parametrs) {
            foreach (var personParametr in person.Parametrs) {
                if (baseParametr.keyWord == personParametr.keyWord &&
                    baseParametr.CurrentData != personParametr.CurrentData) {
                    return false;
                }
            }
        }
        return true;
    }

    public static PersonPreset CopyPersonData (PersonPreset personData) {
        var instance = CreateInstance<PersonPreset> ();
        instance.Parametrs = (PersonParametr[]) personData.Parametrs.Clone ();
        for (int i = 0; i < instance.Parametrs.Length; i++) {
            instance.Parametrs[i] = instance.Parametrs[i].CreateWorkCopy ();
        }
        return instance;
    }

}