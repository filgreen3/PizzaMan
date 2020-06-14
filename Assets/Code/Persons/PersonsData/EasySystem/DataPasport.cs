using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

[SerializeField]
public class DataPasport : IPersonVisual {
    [Space (6)] public List<DataEntity> Elements = new List<DataEntity> ();

    public DataEntity[] Entities => Elements.ToArray ();

    public bool Match (IPersonVisual person) {
        foreach (var baseEntity in Elements) {
            if (!MatchEntity (baseEntity, person.Entities))
                return false;
        }
        return true;
    }

    private bool MatchEntity (DataEntity entity, DataEntity[] entities) {
        foreach (var item in entities) {
            if (item.EntitiesNames[0].Equals (entity.EntitiesNames[0]) ||
                (item.EntitiesNames[0].StartsWith ("hair") && entity.EntitiesNames[0].StartsWith ("hair") && int.Parse (item.EntitiesNames[0].Remove (0, 5)) % 4 == int.Parse (entity.EntitiesNames[0].Remove (0, 5)) % 4)) {
                return true;
            }
        }
        return false;
    }

    public void SettingPerson (Person person) {
        var transfBody = person.transform.GetChild (0);

        for (int i = 0; i < transfBody.childCount; i++) {
            transfBody.GetChild (i).gameObject.SetActive (false);
        }

        foreach (var item in Elements) {
            var entity = item.GetEntity ();
            var entityNamesList = entity.EntitiesNames;
            foreach (var nameElement in entityNamesList) {
                transfBody.Find (nameElement)?.gameObject.SetActive (true);
            }
        }
    }
}