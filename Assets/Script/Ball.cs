using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float ballSpeed;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(-transform.forward * ballSpeed);
       gameObject.GetComponent<Rigidbody>().AddForce(transform.up * ballSpeed);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponentInParent<Frog>() != null)
        {
            Debug.Log("frog collision");
            //collision.gameObject.GetComponentInParent<Frog>().DestroySelf();//调用方法
        }
           
    }
}
