using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersonMatchManager : MonoBehaviour {

    [SerializeField] private PersonPreset MatchData;
    [SerializeField] private DataPasport MatchPasport;

    [SerializeField] private Score scoreManager;

    public static PersonMatchManager instance;

    private void Start () {
        instance = this;
        MatchData.SortParametrs ();
        MatchPasport = MatchData.GetDataPasport ();

        for (int i = 0; i < MatchPasport.Elements.Count; i++) {
            scoreManager.ImageCriteria[i].gameObject.SetActive (true);
            scoreManager.ImageCriteria[i].transform.GetChild (0).GetComponent<Image> ().sprite =
                MatchPasport.Elements[i].Icon;
        }

    }

    public static void MatchPerson (Person person) {
        if (instance.MatchPasport.Match (person.Data)) {
            instance.scoreManager.FlyersGood++;
        } else {
            Debug.Log ("Bad!");
            instance.scoreManager.lossesGood++;
        }
    }

}