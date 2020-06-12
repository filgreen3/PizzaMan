using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuIngame : MonoBehaviour
{
    public GameObject PauseMenu;
    public GameObject PauseMenuMain;
    public GameObject[] PauseMenuall;
    public GameObject MainMenu;
    public Fader fader;
    
    bool timescale0 = false;
    public void Resume()
    {
        
        if (timescale0)
        {
            timescale0 = false;
            Time.timeScale = 1;
        }
        Debug.Log(Time.timeScale);
        for (int i = 0; i < PauseMenuall.Length; i++)
            PauseMenuall[i].SetActive(false);
        PauseMenu.SetActive(false);
    }
    public void Endless(GameObject New)
    {
        New.SetActive(true);
        MainMenu.SetActive(false);

}    
    public void New()
    {
        fader.gameObject.SetActive(true);
        fader.next = 1;

    }    
    public void Menu()
    {
        fader.gameObject.SetActive(true);
        fader.next = 0;

    }
    private void Start()
    {
        Application.targetFrameRate = 60;
    }
    void Update()
    {

        if (Input.GetKeyUp(KeyCode.Escape) && !PauseMenu.activeSelf)
        {
            PauseMenu.SetActive(true);
            if (Time.timeScale > 0)
            {
                Time.timeScale = 0;
                timescale0=true;
            }
            Debug.Log(Time.timeScale);
        }



    }
}
