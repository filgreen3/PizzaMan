using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersonMatchManager : MonoBehaviour {

    [SerializeField] private PersonPreset MatchData;

    [SerializeField] private Transform MatchShowBar;
    [SerializeField] private Transform MatchShowElement;

    public static PersonMatchManager instance;

    private void Start () {
        instance = this;
        MatchData = PersonPreset.CopyPersonData (MatchData);

        foreach (var parametr in MatchData.Parametrs) {
            var element = Instantiate (MatchShowElement, MatchShowBar);
            element.GetChild (0).GetComponent<Image> ().sprite = parametr.CurrentData.Icon;
        }

    }

    public static void MatchPerson (Person person) {
        if (instance.MatchData.Match (person.Data)) {
            Debug.Log ("Cool!");
        } else {
            Debug.Log ("Bad!");
        }
    }

}