using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gold : MonoBehaviour
{
    Transform kid;
    kidKontroller kidKont;
    void Start()
    {
        kid = GameObject.Find("Kid").GetComponent<Transform>();
        kidKont = GameObject.Find("Kid").GetComponent<kidKontroller>();


    }


    void Update()
    {
        float dist = Vector3.Distance(transform.position, kid.position);//çocuk ile arasındaki mesafe
        if (kidKont.magnetTaked)// Mıknatıs var ise altınların üzerine doğru gitmesi için
        {

            if (dist <= 3.0f)
            {
                transform.position = Vector3.MoveTowards(transform.position, kid.position, 10.0f * Time.deltaTime);

            }
            
        }
        if (transform.position.z<kid.position.z)
        {
            gameObject.SetActive(false);
        }
    }
}
