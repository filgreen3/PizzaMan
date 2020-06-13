using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPersonVisual {
    DataEntity[] Entities { get; }

    void SettingPerson (Person person);
    bool Match (IPersonVisual person);
}

[System.Serializable]
public abstract class DataEntityGiver : ScriptableObject {

    public abstract int EntitiesCount { get; }
    public abstract string[] EntitiesNames { get; }

    public int lastIndex;

    public DataEntity GetEntity () => GetEntity (out lastIndex);
    public abstract DataEntity GetEntity (out int index);
}