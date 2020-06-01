using UnityEngine;

[CreateAssetMenu (fileName = "Data", menuName = "ScriptableObjects/PersonParametr", order = 1)]
public class PersonParametr : ScriptableObject {
    public DataParametr CurrentData;
    public DataParametr[] datas;

}
