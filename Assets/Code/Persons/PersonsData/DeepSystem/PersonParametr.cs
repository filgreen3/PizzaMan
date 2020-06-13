using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu (fileName = "Data", menuName = "ScriptableObjects/PersonParametr", order = 1)]
public class PersonParametr : DataEntityGiver {

    public PersonParametr linkedParametr;
    public List<DataEntityGiver> datas = new List<DataEntityGiver> ();

    public override int EntitiesCount =>
        datas.Sum ((data) => data.EntitiesCount);

    public override string[] EntitiesNames =>
        datas.SelectMany (item => item.EntitiesNames).Distinct ().ToArray ();

    public override DataEntity GetEntity (out int index) {
        if (linkedParametr == null) {
            var entityGiver = IEnumerableExtensions.RandomElementByWeight<DataEntityGiver> (datas, item => (float) item.EntitiesCount);
            index = datas.IndexOf (entityGiver);
            lastIndex = index;
            //            Debug.Log (entityGiver);
            return entityGiver.GetEntity ();
        } else {
            index = linkedParametr.lastIndex;
            return datas[index].GetEntity ();
        }
    }
}