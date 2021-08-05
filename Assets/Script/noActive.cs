using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class noActive : MonoBehaviour
{
    
    Transform kid;
    void Start()
    {
        kid = GameObject.Find("Kid").GetComponent<Transform>();
        
    }


    void Update()
    {
  
         if (transform.position.z<kid.position.z)
        {

            gameObject.SetActive(false);
        }
    }
}
