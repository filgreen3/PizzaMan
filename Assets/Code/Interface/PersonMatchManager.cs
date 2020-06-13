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

        scoreManager.ImageCriteria[0].gameObject.SetActive (true);
        scoreManager.ImageCriteria[0].transform.GetChild (0).GetComponent<Image> ().sprite =
            MatchPasport.Elements[0].Icon;
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