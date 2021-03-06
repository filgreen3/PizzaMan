﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

    [Serializable]
public class Dialogue : MonoBehaviour
{
    public Text txt;
    public AudioSource audio;
    public int stage=-1;
    public SpriteRenderer Char1;
    public SpriteRenderer Char2;
    public Image window;
    public GameObject Exit;
    public GameObject PauseMenu;


    bool move = false;
    float t = 0;
    public List<MonsterArray> dialogue;

    public void Close(GameObject go)
    {
        Time.timeScale = 1;
        if (go != null) go.SetActive(true);


        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        Time.timeScale = 0;
        stage = -1;
        Dia();
    }
    public void Dia()
    {
        stage++;
        audio.Stop();
        audio.PlayOneShot(dialogue[stage].sound);
        StopCoroutine("PlayText");
        StartCoroutine("PlayText");
        move = true;
        Char1.sprite = dialogue[stage].sprite1;
        Char2.sprite = dialogue[stage].sprite2;
        window.color = new Color(1, 1, 1, 0);
        t = 0;
        if (dialogue[stage].PlayerReply)
        {
            Char1.transform.localScale = new Vector2(0.5f, 0.4f);
            window.transform.position = new Vector3(Camera.main.transform.position.x-1, -2);
            //window.transform.localScale = new Vector3(-1, 1,1);
            //txt.transform.localScale = new Vector2(-1, 1);
            audio.panStereo = -0.5f;
        }
        else
        {
            Char2.transform.localScale = new Vector2(0.5f, 0.4f);
            window.transform.position = new Vector2(Camera.main.transform.position.x+1, -2);
            //window.transform.localScale = new Vector3(1, 1,1);
            //txt.transform.localScale = new Vector2(1, 1);
            audio.panStereo = 0.5f;
        }
        string text = dialogue[stage].story;
        for (int i = 0; i < dialogue[stage].Activate.Length; i++) dialogue[stage].Activate[i].SetActive(true);
        window.gameObject.GetComponent<RectTransform>().sizeDelta = 
            new Vector2(500,100+LocalizationManager.instance.GetLocalizetedValue(text).Length/25.0f*28);
    }
    private void Update()
    {
        if (Input.GetMouseButtonUp(0)&& !PauseMenu.activeSelf) 
        {
            if (stage + 1 < dialogue.Count) Dia();
            else Exit.SetActive(true);
        }
        if (move)
        {
            if (dialogue[stage].PlayerReply) Char1.transform.localScale=Vector2.Lerp(Char1.transform.localScale, new Vector2(0.5f, 0.5f), Time.unscaledDeltaTime * 10);
            else Char2.transform.localScale = Vector2.Lerp(Char2.transform.localScale, new Vector2(0.5f, 0.5f), Time.unscaledDeltaTime * 10);
            window.transform.position = Vector2.Lerp(window.transform.position, new Vector2 (window.transform.position.x,0), Time.unscaledDeltaTime * 10);
            window.color = Color.Lerp(new Color(1, 1, 1, 0), Color.white, t);
            t += Time.unscaledDeltaTime / 0.5f;
            if (window.transform.position == Vector3.zero) move = false;
        }
    }
    IEnumerator PlayText()
    {
        txt.text = "";
        string text = dialogue[stage].story;

        foreach (char c in LocalizationManager.instance.GetLocalizetedValue(dialogue[stage].story))
        {
            txt.text += c;

            yield return new WaitForSecondsRealtime(0.02f);
        }
        //audio.Stop();
    }
}
    [Serializable]
    public class MonsterArray
    {
    [TextArea]
    public string story;
    public bool PlayerReply;
    //public bool exit=false;
    public AudioClip sound;
    public Sprite sprite1;
    public Sprite sprite2;
    public GameObject[] Activate;
    //public Vector2 scale;
    }