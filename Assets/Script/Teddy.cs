using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teddy : MonoBehaviour
{
    public float ballSpeed;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponentInChildren<Rigidbody>().AddForce(transform.forward * ballSpeed);
        //gameObject.GetComponentInChildren<Rigidbody>().AddForce(transform.up * ballSpeed);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

