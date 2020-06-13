using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersonMatchManager : MonoBehaviour {

    [SerializeField] private PersonPreset MatchData;
    [SerializeField] private DataPasport MatchPasport;

    [SerializeField] private Transform MatchShowBar;
    [SerializeField] private Transform MatchShowElement;

    public static PersonMatchManager instance;

    private void Start () {
        instance = this;
        MatchData.SortParametrs ();
        MatchPasport = MatchData.GetDataPasport ();

        var data = Instantiate (MatchShowElement, MatchShowBar);
        data.GetChild (0).GetComponent<Image> ().sprite =
            MatchPasport.Elements[0].Icon;
    }

    public static void MatchPerson (Person person) {
        if (instance.MatchPasport.Match (person.Data)) {
            Debug.Log ("Cool!");
        } else {
            Debug.Log ("Bad!");
        }
    }

}