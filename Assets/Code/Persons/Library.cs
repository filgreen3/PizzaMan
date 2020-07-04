using UnityEngine;
using System.Linq;
[System.Serializable]
public class Library
{
    public string Lable;
    public bool usePrev;
    public float Weight;
    public DataEntity[] Datas;

    public string[] GetNames(ref int id, ref DataPasport pasport)
    {
        if (!usePrev)
            id = Random.Range(0, Datas.Length);


        if (Random.value > Weight)
        {
            pasport.AddEntity(Datas[id]);
            return Datas[id].EntitiesNames;
        }
        else
        {
            return new string[0];
        }
    }
    public string[] GetAllNames => Datas.SelectMany(item => item.NamesItem).Distinct().ToArray();
}
