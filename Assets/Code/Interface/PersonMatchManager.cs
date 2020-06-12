using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersonMatchManager : MonoBehaviour {

    [SerializeField] private PersonPreset MatchData;
    [SerializeField] private IPersonVisual MatchPasport;

    [SerializeField] private Transform MatchShowBar;
    [SerializeField] private Transform MatchShowElement;

    public static PersonMatchManager instance;

    private void Start () {
        instance = this;
        MatchPasport = MatchData.GetDataPasport ();
    }

    public static void MatchPerson (Person person) {
        if (instance.MatchPasport.Match (person.Data)) {
            Debug.Log ("Cool!");
        } else {
            Debug.Log ("Bad!");
        }
    }

}