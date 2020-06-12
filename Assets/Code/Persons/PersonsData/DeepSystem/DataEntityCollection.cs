using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu (fileName = "Data", menuName = "ScriptableObjects/DataCollection", order = 1)]
public class DataEntityCollection : ScriptableObject {
    public bool ConstainsName (string matchName) => Collection.Any (item => item.EntitiesNames.Contains (matchName));

    public List<DataEntity> Collection = new List<DataEntity> ();
}