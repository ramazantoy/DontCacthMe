using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menuManager : MonoBehaviour
{
    [SerializeField]
    Text score_text;
    [SerializeField]
    Sprite soundoff_png;
    [SerializeField]
    Sprite soundon_png;
    [SerializeField]
    Button soundonof_button;
    [SerializeField]
    AudioSource menu_audio;
    int score;
    bool isSound;

    void Start()
    {
        checksound();
        score = PlayerPrefs.GetInt("score");
        score_text.text = "HIGH SCORE : " + score;
    }
   public void playButton()
    {
        SceneManager.LoadScene(1);
        
    }
    public void quitButton()
    {
        Application.Quit();
    }
    void checksound()
    {
      int  sound_tmp = PlayerPrefs.GetInt("menusound");
        if (sound_tmp == 0)
        {
            isSound = true;
        }
        else
        {
            isSound = false;
           
        }
    }
    public void setSound()
    {
        if (isSound)
        {
            soundonof_button.GetComponent<Image>().sprite = soundoff_png;

            menu_audio.Pause();
            isSound = false;
            PlayerPrefs.SetInt("menusound", 1);//ses acik
        }
        else
        {
            soundonof_button.GetComponent<Image>().sprite = soundon_png;
            menu_audio.UnPause();
            isSound = true;
            PlayerPrefs.SetInt("menusound", 0);//ses acik
        }
    }
    void Update()
    {
        
    }
}
