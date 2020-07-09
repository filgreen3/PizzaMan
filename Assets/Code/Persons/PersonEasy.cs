using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PersonEasy : Person
{
    public Library[] Datas;
    public string[] AllOn;

    public int matchId;

    public override void Init()
    {
        base.Init();
        var names = new List<string>();
        pasport = new DataPasport();

        int id = 0;
        foreach (var item in Datas)
        {
            names.AddRange(item.GetNames(ref id, ref pasport));
        }


        var transfBody = transform.GetChild(0);

        for (int i = 0; i < transfBody.childCount; i++)
        {
            transfBody.GetChild(i).gameObject.SetActive(false);
        }
        foreach (var name in names)
        {
            transfBody.Find(name)?.gameObject.SetActive(true);
        }
        foreach (var name in AllOn)
        {
            transfBody.Find(name)?.gameObject.SetActive(true);
        }
    }




    public void MySetting(string[] namesDisable, string[] namesOn)
    {
        var transfBody = transform.GetChild(0);
        foreach (var name in namesDisable)
        {
            transfBody.Find(name)?.gameObject.SetActive(false);
        }
        foreach (var name in namesOn)
        {
            transfBody.Find(name)?.gameObject.SetActive(true);
        }
    }

    protected override void OnRebuild()
    {
        var mine = this;
        PersonMatchManager.instance.LoadTruePerson(Random.value > 0.3f, ref mine);
    }
}


