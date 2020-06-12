using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public float time; 
    float StartTime;
    public int lossesGood=0;
    public int lossesBad=0;
    public int FlyersGood=5;
    public int FlyersBad=5;
    public GameObject Fired;
    public Image[] ImageCriteria;
    public Image[] Criteria;
    public Text ScoreGood;
    public Text ScoreBad;
    public Transform TimeLine;
    public GameObject[] PenaltyGood;
    public GameObject[] PenaltyBad;
    
    void Start()
    {
        StartTime = time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        TimeLine.localScale = new Vector3(time / StartTime, 1, 1);
        time -= Time.fixedDeltaTime;
        if (lossesGood > 0) PenaltyGood[lossesGood - 1].SetActive(true);
        if (lossesBad > 0) PenaltyBad[lossesBad - 1].SetActive(true);
        if (lossesGood > 2 || lossesBad > 2 || time < 0) Fired.SetActive(true);
    }
}
