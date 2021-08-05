using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class car : MonoBehaviour
{
    float speed = 2.0f;
    [SerializeField]
    AudioSource car_audio;
    Transform kid;


    void Start()
    {
        kid = GameObject.Find("Kid").GetComponent<Transform>();
    }

   
    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
        float dist = Vector3.Distance(transform.position, kid.position);//çocuk ile arasındaki mesafe
            if (dist <= 8.0f)
            {
           // Debug.Log("car alert");
            car_audio.UnPause();

            }
        else
        {
            car_audio.Pause();
        }

        
    }
}
