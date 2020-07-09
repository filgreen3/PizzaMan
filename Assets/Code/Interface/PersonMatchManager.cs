using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PersonMatchManager : MonoBehaviour
{
    public DataPasport MatchPasportGood;
    public DataPasport MatchPasportBad;
    public GameObject badpanel;

    public LibraryContainer[] GoodLibrary;
    public LibraryContainer[] BadLibrary;

    public AudioSource audio;
    public AudioClip Win;
    public AudioClip Loose;
    public bool endless = false;
    public Score scoreManager;

    [SerializeField] private Transform playerMouse;
    [HideInInspector] public Person[] PersonTransforms;

    public int matcherIdgood;
    public int matcherIdbad;


    public static PersonMatchManager instance;


    [ContextMenu("CopyLibs")]
    public void CopyLibrarys()
    {
        BadLibrary = (LibraryContainer[])GoodLibrary.Clone();
    }

    private void Start()
    {
        instance = this;

        UpdateMatcher(true);
        UpdateMatcher(false);
        audio = GetComponent<AudioSource>();
    }

    public void LoadTruePerson(bool isGood, ref PersonEasy person)
    {
        if (person.matchId == (isGood ? matcherIdgood : matcherIdbad))
        {
            var list1 = isGood ? GoodLibrary[matcherIdgood].libraries.SelectMany(item => item.GetAllNames).ToArray() : BadLibrary[matcherIdbad].libraries.SelectMany(item => item.GetAllNames).ToArray();

            var trueEntity = isGood ? MatchPasportGood.entities : MatchPasportBad.entities;
            var list2 = trueEntity.SelectMany(item => item.NamesItem).ToArray();


            person.MySetting(list1, list2);
            foreach (var item in trueEntity)
            {
                person.pasport.Elements.Add(item.Icon);
            }
        }
    }

    void GetMatcher(bool isGood, ref DataPasport personVisual)
    {
        int id = 0;
        Library[] list = null;

        if (isGood)
        {
            matcherIdgood = Random.Range(0, 2);
            list = GoodLibrary[matcherIdgood].libraries;
        }
        else
        {
            matcherIdbad = Random.Range(0, 2);
            list = BadLibrary[matcherIdbad].libraries;
        }
        foreach (var item in list)
        {
            item.GetNames(ref id, ref personVisual);
        }
    }



    public void MatchPerson(Person person, bool isGood)
    {
        if (isGood)
        {
            if (MatchPasportGood.Match(person.pasport) >= GoodLibrary[matcherIdgood].libraries.Length)
            {
                Debug.Log("Yes!");
                if (endless) { scoreManager.FlyersGood++; scoreManager.time += 20; } else scoreManager.FlyersGood--;
                UpdateMatcher(true);
                audio.PlayOneShot(Win);
            }
            else
            {
                Debug.Log("No!");
                scoreManager.lossesGood++;
                UpdateMatcher(true);
                audio.PlayOneShot(Loose);
            }
        }
        else
        {
            if (MatchPasportBad.Match(person.pasport) >= BadLibrary[matcherIdbad].libraries.Length)
            {
                Debug.Log("Yes!");
                if (badpanel.activeSelf)
                {
                    if (endless) { scoreManager.FlyersBad++; scoreManager.time += 20; } else scoreManager.FlyersBad--;
                    UpdateMatcher(false);
                    audio.PlayOneShot(Win);
                }
            }
            else
            {
                Debug.Log("No!");
                if (badpanel.activeSelf)
                {
                    scoreManager.lossesBad++;
                    UpdateMatcher(false);
                    audio.PlayOneShot(Loose);
                }
            }
        }
    }

    public void UpdateMatcher(bool isGood)
    {
        if (isGood)
        {
            MatchPasportGood = new DataPasport();
            instance.GetMatcher(true, ref MatchPasportGood);
            for (int i = 0; i < Mathf.Min(scoreManager.GoodParamsCount, MatchPasportGood.Elements.Count); i++)
            {
                scoreManager.ImageCriteria[i].gameObject.SetActive(true);
                scoreManager.ImageCriteria[i].transform.GetChild(0).GetComponent<Image>().sprite =
                    MatchPasportGood.Elements[i];
            }
        }
        else
        {
            MatchPasportBad = new DataPasport();
            instance.GetMatcher(false, ref MatchPasportBad);
            for (int i = 0; i < Mathf.Min(scoreManager.BadParamsCount, MatchPasportBad.Elements.Count); i++)
            {
                scoreManager.ImageCriteria[i + 3].gameObject.SetActive(true);
                scoreManager.ImageCriteria[i + 3].transform.GetChild(0).GetComponent<Image>().sprite =
                    MatchPasportBad.Elements[i];
            }
        }
    }

    public static void Match(bool isGood)
    {
        if (Time.timeScale <= 0) return;
        Vector2 dist;
        var list = instance.PersonTransforms.
        Where(item =>
        {
            dist = (item.transform.position - instance.playerMouse.position);
            return dist.magnitude < 2f;
        }).ToArray();

        Debug.Log(list.Length);

        foreach (var item in list)
        {
            if (isGood)
            {
                if (instance.MatchPasportGood.Match(item.pasport) >= instance.scoreManager.GoodParamsCount)
                {
                    instance.MatchPerson(item, isGood);
                    return;
                }
            }
            else
            {
                if (instance.MatchPasportBad.Match(item.pasport) >= instance.scoreManager.BadParamsCount)
                {
                    instance.MatchPerson(item, isGood);
                    return;
                }
            }
        }
        if (list.Length > 0)
            instance.MatchPerson(list[0], isGood);
    }
}
[System.Serializable]
public class LibraryContainer
{
    public string Lable;
    public Library[] libraries;
}