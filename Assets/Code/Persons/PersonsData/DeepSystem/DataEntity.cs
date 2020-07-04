using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Entity", menuName = "ScriptableObjects/DataEntity", order = 1)]
public class DataEntity : DataEntityGiver
{
    public string Key { get; private set; }

    public void Init(string key)
    {
        Key = key;
        NamesItem = new string[] { key };
    }

    public void AddNames(string names)
    {
        Array.Resize(ref NamesItem, NamesItem.Length + 1);
        NamesItem[NamesItem.Length - 1] = names;
    }

    public Sprite Icon;
    public string[] NamesItem;

    public override int EntitiesCount => 1;
    public override string[] EntitiesNames => NamesItem;

    public override DataEntity GetEntity(out int index)
    {
        index = 0;
        return this;
    }
}