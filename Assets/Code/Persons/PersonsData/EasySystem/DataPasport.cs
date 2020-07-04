using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable, SerializeField]
public class DataPasport : IPersonVisual
{
    [Space(6)] public List<Sprite> Elements = new List<Sprite>();
    [Space(6)] public List<DataEntity> entities = new List<DataEntity>();

    public Sprite[] Entities => Elements.ToArray();

    public void AddEntity(DataEntity entity)
    {
        Elements.Add(entity.Icon);
        entities.Add(entity);
    }

    public int Match(IPersonVisual person)
    {
        var list1 = Entities;
        var list2 = person.Entities;

        var count = list1.Intersect(list2).Count();
        Debug.Log(count);

        return count;
    }
}