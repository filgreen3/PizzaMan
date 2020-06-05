using UnityEngine;

[CreateAssetMenu (fileName = "Data", menuName = "ScriptableObjects/PersonParametr", order = 1)]
public class PersonParametr : ScriptableObject {
    public string keyWord;
    public DataEntity CurrentData;
    public DataEntity[] datas;

    [SerializeField] private bool Origin;

    public PersonParametr CreateWorkCopy () {

        if (!Origin) {
            Debug.LogError ("Try to create copy not from origin");
            return null;
        }

        if (datas.Length == 0) {
            Debug.LogError ("Try to create copy from empty datastorage");
            return null;
        }

        var instance = CreateInstance<PersonParametr> ();
        instance.CurrentData = this.datas[Random.Range (0, datas.Length)];
        instance.keyWord = keyWord;
        return instance;
    }

}