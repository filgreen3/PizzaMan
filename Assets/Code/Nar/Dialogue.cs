using System.Collections;
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
    bool move = false;
    float t = 0;
    public List<MonsterArray> dialogue;

    public void Close(GameObject go)
    {
        go.SetActive(true);
        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
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
            window.transform.position = new Vector2(-1, -2);
            window.transform.localScale = new Vector2(-1, 1);
            txt.transform.localScale = new Vector2(-1, 1);
            audio.panStereo = -0.5f;
        }
        else
        {
            Char2.transform.localScale = new Vector2(0.5f, 0.4f);
            window.transform.position = new Vector2(1, -2);
            window.transform.localScale = new Vector2(1, 1);
            txt.transform.localScale = new Vector2(1, 1);
            audio.panStereo = 0.5f;

        }
        window.gameObject.GetComponent<RectTransform>().sizeDelta = dialogue[stage].scale;

    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0)) 
        {
            if (stage + 1 < dialogue.Count) Dia();
            else Exit.SetActive(true);
        }
        if (move)
        {
            if (dialogue[stage].PlayerReply) Char1.transform.localScale=Vector2.Lerp(Char1.transform.localScale, new Vector2(0.5f, 0.5f), Time.deltaTime * 10);
            else Char2.transform.localScale = Vector2.Lerp(Char2.transform.localScale, new Vector2(0.5f, 0.5f), Time.deltaTime * 10);
            window.transform.position = Vector2.Lerp(window.transform.position, new Vector2 (window.transform.position.x,0), Time.deltaTime * 10);
            window.color = Color.Lerp(new Color(1, 1, 1, 0), Color.white, t);
            t += Time.deltaTime / 0.5f;
            if (window.transform.position == Vector3.zero) move = false;
        }
    }

    IEnumerator PlayText()
    {
        txt.text = "";
        foreach (char c in dialogue[stage].story)
        {
            txt.text += c;
            yield return new WaitForSeconds(0.02f);
        }
    }
}
    [Serializable]
    public class MonsterArray
    {
        public string story;
        public bool PlayerReply;
        public bool exit=false;
        public AudioClip sound;
    public Sprite sprite1;
    public Sprite sprite2;
    public Vector2 scale;

    }