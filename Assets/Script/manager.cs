using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class manager : MonoBehaviour
{
    [SerializeField]
    Button sound_onof_button;
    bool soundOn;
    [SerializeField]
    GameObject game_stop_pnl;
    [SerializeField]
    Sprite music_off_png;
    [SerializeField]
    Sprite music_on_png;
    [SerializeField]
    Button play_pause_button;
    public GameObject gold;//altın
    public GameObject ston;//odun
    public GameObject car;//araba
    public GameObject magnet;//mıknatıs
    List<GameObject> Golds;
    List<GameObject> Others;
    [SerializeField]
    Text score_txt;
    [SerializeField]
    Transform kid;
    [SerializeField]
    AudioSource audio;
    [SerializeField]
    AudioSource audio_gold;
    [SerializeField]
    AudioClip gold_earn_sound;
    [SerializeField]
    Animator kid_animator;
   public int score = 0;
    void Start()
    {
        kid_animator.speed = 1;
        checkSound();
        CancelInvoke("goldGenerete");
        CancelInvoke("otherGenerete");
        CancelInvoke("seedUP");
        score = 0;
        score_txt.text = "" + score;
        Golds = new List<GameObject>();
        Others = new List<GameObject>();
        Generete(gold,10,Golds);
        Generete(ston, 5, Others);
        Generete(car, 2, Others);
        Generete(magnet, 3, Others);
        InvokeRepeating("goldGenerete", 0.0f, 1f);
        InvokeRepeating("otherGenerete", 2.0f, 3f);
        InvokeRepeating("speedUp", 20f, 20f);
        play_pause_button.gameObject.SetActive(true);
        game_stop_pnl.SetActive(false);
        Time.timeScale = 1.0f;
      
    }
    void speedUp()
    {
        kidKontroller.run_speed += 1.0f;
        kid_animator.speed += 0.5f;
    }

    void Generete(GameObject newbie,int w,List<GameObject>L)
    {
        
        for(int i = 0; i< w; i++)
        {
            GameObject newbie_tmp = Instantiate(newbie);
            newbie_tmp.SetActive(false);
            L.Add(newbie_tmp);
        }
    }
    void goldGenerete()
    {
        foreach(GameObject gold in Golds)
        {
            if (gold.activeSelf == false)//gold aktif değil ise
            {
                gold.SetActive(true);
                int r = Random.Range(0, 2);
                if (r == 0)
                {
                    gold.transform.position = new Vector3(1.6f, 0.4f,kid.position.z+10.0f);//ana karakterin 10 birim ötesinde 
                }
               else
                {
                    gold.transform.position = new Vector3(-1.6f, 0.4f, kid.position.z + 10.0f);//ana karakterin 10 birim ötesinde 
                }
                return;// döngüyü durdurmak amacıyla

            }
        }
    }
   
    void otherGenerete()
    {
        int r = Random.Range(0, Others.Count);
        if (Others[r].activeSelf == false)
        {
            Others[r].SetActive(true);
            int ra = Random.Range(0, 2);
            if (ra == 0)
            {
                Others[r].transform.position = new Vector3(1.6f, 0f, kid.position.z + 10.0f);//ana karakterin 10 birim ötesinde 
            }
            else
            {
                Others[r].transform.position = new Vector3(-1.6f, 0f, kid.position.z + 10.0f);//ana karakterin 10 birim ötesinde 
            }
           
                if (Others[r].tag == "magnet")
                {
                Others[r].transform.position += new Vector3(0f, 0.5f, 0f);
                if (kid.gameObject.GetComponent<kidKontroller>().magnetTaked == true)
                {
                    Others[r].SetActive(false);
                }
                  
                }
                if(Others[r].name== "Car(Clone)")
                {
                Others[r].transform.position += new Vector3(0f, 0.093f, 0f);
                   }
            
           
        }
        else
        {
            foreach(GameObject other in Others)
            {
                if (other.activeSelf == false)
                {
                    other.SetActive(true);
                    int ra = Random.Range(0, 2);
                    if (ra == 0)
                    {
                         other.transform.position = new Vector3(1.6f, 0f, kid.position.z + 10.0f);//ana karakterin 10 birim ötesinde 
                    }
                    else
                    {
                        other.transform.position = new Vector3(-1.6f, 0f, kid.position.z + 10.0f);//ana karakterin 10 birim ötesinde 
                    }
                    
                        if (other.tag == "magnet")
                        {
                        other.transform.position += new Vector3(0f, 0.5f, 0f);
                        if (kid.gameObject.GetComponent<kidKontroller>().magnetTaked == true)
                        {
                            other.SetActive(false);
                           
                        }
                       
                    }
                    if (other.name == "Car(Clone)")
                    {
                        Others[r].transform.position += new Vector3(0f, 0.093f, 0f);
                    }



                    return;
                }
            }
         
        }
       
    }
    public void scoreUp(int s)
    {
        audio_gold.PlayOneShot(gold_earn_sound);
        score += s;
        score_txt.text = "" + score;
    }
    public void playPauseButton()
    {
        if (play_pause_button.gameObject.activeSelf == true)
        {
      
            Time.timeScale = 0.0f;
            play_pause_button.gameObject.SetActive(false);
            game_stop_pnl.SetActive(true);
        }

    }
    public void playButton()
    {
        Time.timeScale = 1.0f;
        play_pause_button.gameObject.SetActive(true);
        game_stop_pnl.SetActive(false);
    }
    public void soundButton()
    {
        if (soundOn == true)
        {
            PlayerPrefs.SetInt("sound", 0);//ses kapalı
            sound_onof_button.gameObject.GetComponent<Image>().sprite = music_off_png;
            soundOn = false;
            Debug.Log("sound off");
            audio.Stop();
        }
        else 
        {
            PlayerPrefs.SetInt("sound", 1);//ses acik
            sound_onof_button.gameObject.GetComponent<Image>().sprite = music_on_png;
            audio.Play();
            soundOn = true;
           Debug.Log("sound on");
          

        }
    }
    void checkSound()
    {
        int sound = PlayerPrefs.GetInt("sound");
      //  Debug.Log(sound);
        if (sound == 0)
        {
            soundOn = false;
        }
        else if(sound==1)
        {
            soundOn = true;
        }

        if (soundOn == false)//Muzik kapalı ise
        {
            audio.Pause();
            sound_onof_button.gameObject.GetComponent<Image>().sprite = music_off_png;

        }
        else
        {
            audio.UnPause();
            sound_onof_button.gameObject.GetComponent<Image>().sprite = music_on_png;
        }

    }
    public void restartButton()
    {
        SceneManager.LoadScene(1);
    }
    public void quitButton()
    {
        Application.Quit();
    }
    public void menuButton()

    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }

    void Update()
    {
        
    }
}
