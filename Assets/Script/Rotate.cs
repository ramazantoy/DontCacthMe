using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{

    float speed =500f;
    string namee;
    void Start()
    {
        namee = gameObject.tag;
        
    }

  
    void Update()
    {

        if (namee == "magnet")
        {
            gameObject.transform.Rotate(speed * Time.deltaTime, 0,0);
        }
        else
        {
            gameObject.transform.Rotate(0, speed * Time.deltaTime, 0,Space.World);
        }
   
    }
}
