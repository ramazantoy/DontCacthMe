using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerafallow : MonoBehaviour
{
    [SerializeField]
    Transform kid;
    Vector3 distance;
    float speed = 4.0f;//kameranın takip etme hızı
    void Start()
    {
        
    }
    private void LateUpdate()
    {
        distance = new Vector3(kid.position.x, transform.position.y, kid.position.z - 2.116f);
        transform.position = Vector3.Lerp(transform.position, distance, speed*Time.deltaTime);
        

        
    }
}
