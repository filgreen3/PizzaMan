using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu (fileName = "Data", menuName = "ScriptableObjects/PersonData", order = 1)]
public class PersonPreset : ScriptableObject {

    public PersonParametr IncludeParametr;
    public DataEntityGiver[] Parametrs;

    private List<DataEntityGiver> standartParametrs;
    private List<PersonParametr> linkedParametrs;

    public DataPasport GetDataPasport () {
        var pasport = new DataPasport ();

        if (IncludeParametr != null)
            pasport.Elements.AddRange (IncludeParametr.datas.Select (item => item.GetEntity ()));
        pasport.Elements.AddRange (standartParametrs.Select (item => item.GetEntity ()).Distinct ());
        pasport.Elements.AddRange (linkedParametrs.Select (item => item.GetEntity ()).Distinct ());
        return pasport;
    }

    public void SortParametrs () {
        standartParametrs.Clear ();
        linkedParametrs.Clear ();

        foreach (var item in Parametrs) {
            if (item is PersonParametr && ((PersonParametr) item).linkedParametr != null) {
                linkedParametrs.Add ((PersonParametr) item);
            } else {
                standartParametrs.Add (item);
            }
        }

    }

}