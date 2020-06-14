using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PersonMatchManager : MonoBehaviour {

    public PersonPreset MatchData;

    public DataPasport MatchPasportGood;
    public DataPasport MatchPasportBad;
    public GameObject badpanel;

    AudioSource audio;
    public AudioClip Win;
    public AudioClip Loose;
    public bool endless = false;
    public Score scoreManager;

    [SerializeField] private Transform playerMouse;

    [HideInInspector] public Person[] PersonTransforms;

    public static PersonMatchManager instance;

    private void Start () {
        instance = this;
        MatchData.SortParametrs ();

        UpdateMatcher (true);
        UpdateMatcher (false);
        audio = GetComponent<AudioSource> ();
    }

    public static void MatchPerson (Person person, bool isGood) {

        if (isGood) {
            if (instance.MatchPasportGood.Match (person.Data)) {
                Debug.Log ("Good!");
                if (instance.endless) { instance.scoreManager.FlyersGood++; instance.scoreManager.time += 20; } else instance.scoreManager.FlyersGood--;
                instance.UpdateMatcher (true);
                instance.audio.PlayOneShot (instance.Win);
            } else {
                Debug.Log ("Bad!");
                instance.scoreManager.lossesGood++;
                instance.UpdateMatcher (true);
                instance.audio.PlayOneShot (instance.Loose);
            }
        } else {
            if (instance.MatchPasportBad.Match (person.Data)) {
                Debug.Log ("Good!");
                if (instance.badpanel.activeSelf) {
                    if (instance.endless) { instance.scoreManager.FlyersBad++; instance.scoreManager.time += 20; } else instance.scoreManager.FlyersBad--;
                    instance.UpdateMatcher (false);
                    instance.audio.PlayOneShot (instance.Win);
                }
            } else {
                Debug.Log ("Bad!");
                if (instance.badpanel.activeSelf) {
                    instance.scoreManager.lossesBad++;
                    instance.UpdateMatcher (false);
                    instance.audio.PlayOneShot (instance.Loose);
                }
            }
        }
    }

    public void UpdateMatcher (bool isGood) {
        if (isGood) {
            instance.MatchPasportGood = instance.MatchData.GetDataPasport ();
            for (int i = 0; i < Mathf.Min (scoreManager.GoodParamsCount, MatchPasportGood.Elements.Count); i++) {
                scoreManager.ImageCriteria[i].gameObject.SetActive (true);
                scoreManager.ImageCriteria[i].transform.GetChild (0).GetComponent<Image> ().sprite =
                    MatchPasportGood.Elements[i].Icon;
            }
        } else {
            instance.MatchPasportBad = instance.MatchData.GetDataPasport ();
            for (int i = 0; i < Mathf.Min (scoreManager.BadParamsCount, MatchPasportBad.Elements.Count); i++) {
                scoreManager.ImageCriteria[i + 3].gameObject.SetActive (true);
                scoreManager.ImageCriteria[i + 3].transform.GetChild (0).GetComponent<Image> ().sprite =
                    MatchPasportBad.Elements[i].Icon;
            }
        }
    }

    public static void Match (bool isGood) {
        if (Time.timeScale <= 0) return;
        Vector2 dist;
        var list = instance.PersonTransforms.
        Where (item => {
            dist = (item.transform.position - instance.playerMouse.position);
            return dist.magnitude < 2f;
        }).ToArray ();

        Debug.Log (list.Length);

        foreach (var item in list) {
            if (isGood) {
                if (instance.MatchPasportGood.Match (item.Data)) {
                    MatchPerson (item, isGood);
                    return;
                }
            } else {
                if (instance.MatchPasportBad.Match (item.Data)) {
                    MatchPerson (item, isGood);
                    return;
                }
            }
        }
        if (list.Length > 0)
            MatchPerson (list[0], isGood);
    }
}