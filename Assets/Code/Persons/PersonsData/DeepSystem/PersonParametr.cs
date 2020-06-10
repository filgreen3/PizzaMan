using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu (fileName = "Data", menuName = "ScriptableObjects/PersonParametr", order = 1)]
public class PersonParametr : ScriptableObject {
    public string keyWord;
    public DataEntity CurrentData;
    public List<DataEntity> datas = new List<DataEntity> ();

    [SerializeField] private bool Origin;

    public PersonParametr CreateWorkCopy () {

        if (!Origin) {
            Debug.LogError ("Try to create copy not from origin");
            return null;
        }

        if (datas.Count == 0) {
            Debug.LogError ("Try to create copy from empty datastorage");
            return null;
        }

        var instance = CreateInstance<PersonParametr> ();
        instance.CurrentData = this.datas[Random.Range (0, datas.Count)];
        instance.keyWord = keyWord;
        return instance;
    }

    public bool ContainData (string nameData) {
        foreach (var item in datas) {
            if (item.NamesItem == nameData)
                return true;
        }
        return false;
    }
}