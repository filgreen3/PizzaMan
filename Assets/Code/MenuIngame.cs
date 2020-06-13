using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MenuIngame : MonoBehaviour
{
    public GameObject PauseMenu;
    public Toggle Sounds;
    public Toggle Musics;
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
    public void Music(AudioMixer audio)
    {
        if (PlayerPrefs.GetFloat("Music") == 0) { audio.SetFloat("Music", -80); PlayerPrefs.SetFloat("Music", 1); }
        else { audio.SetFloat("Music", 0); PlayerPrefs.SetFloat("Music",0); }
    }    
    public void Sound()
    {
        if (PlayerPrefs.GetFloat("Sound") != 0)
        {
            AudioListener.pause = false; PlayerPrefs.SetFloat("Sound", 0);
        }
        else
        {
            AudioListener.pause = true;
            PlayerPrefs.SetFloat("Sound", 1);
        }
    }    
    public void Lang()
    {
        if (PlayerPrefs.GetString("Language") == "Russian") PlayerPrefs.SetString("Language", "English");
        else PlayerPrefs.SetString("Language", "Russian");
        if (PlayerPrefs.GetString("Language") == null)
        {
            if (Application.systemLanguage == SystemLanguage.Russian) PlayerPrefs.SetString("Language", "English");
            else PlayerPrefs.SetString("Language", "Russian");
        }
        if (PlayerPrefs.GetString("Language") == "Russian") LocalizationManager.instance.LoadLocalizatedText(LocalizationManager.instance.libRU);
        else LocalizationManager.instance.LoadLocalizatedText(LocalizationManager.instance.libEN);
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }    
    public void Menu()
    {
        fader.gameObject.SetActive(true);
        fader.next = 0;

    }
    private void Start()
    {
        
        Application.targetFrameRate = 60;
        if (PlayerPrefs.GetFloat("Music") != 0) Musics.isOn = false;
        if (PlayerPrefs.GetFloat("Sound") != 0) Sounds.isOn = false;
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
