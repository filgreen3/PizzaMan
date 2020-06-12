using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

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
            if (item.Key.Equals (entity.Key))
                return true;
        }
        return false;
    }

    public void SettingPerson (Person person) {

        Debug.Log ("Start", person.gameObject);

        var transfBody = person.transform.GetChild (0);

        for (int i = 0; i < transfBody.childCount; i++) {
            transfBody.GetChild (i).gameObject.SetActive (false);
        }

        foreach (var item in Elements) {
            var entity = item.GetEntity ();
            var entityNamesList = entity.EntitiesNames;
            Debug.Log (entityNamesList.Count (), entity);
            foreach (var nameElement in entityNamesList) {
                transfBody.Find (nameElement).gameObject.SetActive (true);
                Debug.Log (nameElement, person.gameObject);
            }
        }
    }
}