using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Score : MonoBehaviour {
    public bool endless;
    public float time;
    float StartTime;

    public int lossesGood = 0;
    public int lossesBad = 0;
    public int FlyersGood = 5;
    public int FlyersBad = 5;

    public int GoodParamsCount = 5;
    public int BadParamsCount = 5;

    public GameObject FiredBoss;
    public GameObject WinBoss;
    public GameObject FiredBad;
    public Fader fader;
    public Image[] ImageCriteria;
    public Image[] Criteria;
    public Text ScoreGood;
    public Text ScoreBad;
    public Transform TimeLine;
    public GameObject[] PenaltyGood;
    public GameObject[] PenaltyBad;

    void Start () {
        StartTime = time;
    }

    // Update is called once per frame
    void FixedUpdate () {
        TimeLine.localScale = new Vector3 (time / StartTime, 1, 1);
        ScoreGood.text = FlyersGood.ToString();
        ScoreBad.text = FlyersBad.ToString();
        time -= Time.fixedDeltaTime;
        if (lossesGood > 0&& lossesGood < 4) PenaltyGood[lossesGood - 1].SetActive (true);
        if (lossesBad > 0&& lossesBad < 4) PenaltyBad[lossesBad - 1].SetActive (true);
        if (lossesGood > 2 || time < 0) FiredBoss.SetActive (true);
        if (lossesBad > 2) FiredBad.SetActive (true);
        if (FlyersGood == 0 && FlyersBad == 0) {
            fader.gameObject.SetActive (true);
            fader.next = SceneManager.GetActiveScene ().buildIndex + 1;
        }
    }
}