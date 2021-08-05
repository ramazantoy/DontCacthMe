using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kidKontroller : MonoBehaviour
{
    [SerializeField]
    Rigidbody r;
    [SerializeField]
    Animator a;
    [SerializeField]
    Transform way1;
    [SerializeField]
    Transform way2;
    [SerializeField]
    GameObject gameover_pnl;
    float jump_power = 5.0f;
  public static  float run_speed = 2.0f;
    float sight_speed = 3.0f;//sola sağa kayma hızı
    bool left = false;
    bool right = false;
    bool up = false;
    bool isJump = false;// particle efekti açıp kapamak amacıyla
    [SerializeField]
    GameObject particle;//toz 
    [SerializeField]
    AudioSource kid_audio;
    [SerializeField]
    AudioClip jump_audio;
    [SerializeField]
    AudioSource kid_audio_over;
    [SerializeField]
    AudioClip game_over_sound;
    manager m;
    public bool magnetTaked = false;
    void Start()
    {
        run_speed = 2.0f;
        gameObject.SetActive(true);
        m = GameObject.Find("manager").GetComponent<manager>();
        gameover_pnl.SetActive(false);
    }
    //-1.7  ile 1.7

    void Update()
    {
        moveJobs();
      
        
    }
    void moveJobs()
    {
        //parmağın hareket ettiği yönü anlamak amacıyla  delta.position metotdunu kullanacağım pozitif değerler 
        //yukarı ve sağı negatif değerler ise sol ve aşağı yönü kast eder.
        if (Input.touchCount > 0)//ekrana en az bir dokunma var ise
        {
            Touch finger = Input.GetTouch(0);//parmağın bir kez dokunduğu yerin bilgisi
            if (finger.deltaPosition.x > 50.0f)//pozitif 100 piksellik hareket var ise parmak sağa doğru kaydırılmış ise
            {
                right = true;
                left = false;
                up = false;
            }
            if (finger.deltaPosition.x < -50.0f)//pozitif 100 piksellik hareket var ise parmak sola doğru kaydırılmış ise
            {
                right = false;
                left = true;
                up = false;
            }
            if (finger.deltaPosition.y > 100 && isJump==false)// yukarı doğru çekme var ise 100piksellik ve zıplamıyor ise
            {
                right = false;
                left = false;
                up = true;
            }
        }
        if (right)//sağa hareket var ise
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(2f, transform.position.y, transform.position.z), sight_speed * Time.deltaTime);
            right = false;
        }
        if (left)//sola hareket var ise
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(-2f, transform.position.y, transform.position.z), sight_speed * Time.deltaTime);
            left = false;
        }
        if (up==true && isJump==false)//yukarı çekildiyse ve karakter hali hazırda zıplamıyor ise
        {
           // Debug.Log("Jumping");
            r.velocity = Vector3.zero;
            r.velocity = Vector3.up * jump_power;
            up = false;
            isJump = true;
            kid_audio_over.PlayOneShot(jump_audio);
            //Debug.Log("jump anim");
            a.SetTrigger("jump");

        }
        transform.Translate(0, 0, run_speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "way1")
        {
            way2.position = new Vector3(way2.position.x, way2.position.y, way1.position.z + 10);
        }
       if (other.gameObject.name == "way2")
        {
            way1.position = new Vector3(way1.position.x, way1.position.y, way2.position.z + 10);
        }
        if (other.gameObject.tag == "gold")
        {
            other.gameObject.SetActive(false);
            m.scoreUp(10);
        }
        if (other.gameObject.tag == "magnet")
        {
            other.gameObject.SetActive(false);
            magnetTaked = true;
            kid_audio.Play();
            Invoke("magnetClose", 10.0f);
        }

    }
    void magnetClose()
    {
        kid_audio.Stop();
        magnetTaked = false;
    }







    private void OnCollisionStay(Collision collision)//çarpışma devam ediyorken
    {
        isJump = false;

        if (particle.activeSelf == false)
        {
            particle.SetActive(true);
        }

    }
    private void OnCollisionExit(Collision collision)//çarpışma bitti ise
    {
   
     
        isJump = true;
        if (particle.activeSelf == true)
        {
            particle.SetActive(false);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "over")//game over
        {
            kid_audio_over.PlayOneShot(game_over_sound);
            run_speed = 0f;
            a.Play("iddle");
            Invoke("openPnl", 0.4f);
            int score_tmp = PlayerPrefs.GetInt("score");
            if (m.score > score_tmp)
            {
                PlayerPrefs.SetInt("score", m.score);
            }
        }
    }
    void openPnl()
    {
        Time.timeScale = 0;
        gameover_pnl.SetActive(true);
    
    }
}
