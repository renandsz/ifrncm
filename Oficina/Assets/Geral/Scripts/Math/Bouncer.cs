using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public enum TimeType
{
    normalT,
    fixedT
}
public class Bouncer : MonoBehaviour
{
    private Rigidbody2D rb;
    //public float forceValue = 0;

    public GameObject flash;

    public float torque;

    public TimeType tipo;
    //public float treshold = 0.1f;
   
    void Start()
    {
        TryGetComponent(out rb);
    }

    
    
    void Update()
    {
        if (tipo != TimeType.normalT) return;

        rb.angularVelocity = torque;//= (torque*Time.deltaTime);
        Flash();

        
    }
    private void FixedUpdate()
    {
        if (tipo != TimeType.fixedT) return;
        rb.angularVelocity = torque;//(torque *Time.fixedDeltaTime);
                                    // AntiFlash();
        Flash();
    }

    void Flash()
    {
        if (transform.eulerAngles.z is >= 0 and <=180)
        {
          //  flash.GetComponent<SpriteRenderer>().enabled = true;
          flash.SetActive(true);
          flash.GetComponent<SpawnOnClick>().Spawn();
        }
        else
        {
          //  flash.GetComponent<SpriteRenderer>().enabled = false;
          flash.SetActive(false);
        }
    }
    void AntiFlash()
    {
        if (transform.eulerAngles.z is < 360 and >180)
        {
            //  flash.GetComponent<SpriteRenderer>().enabled = true;
            flash.SetActive(true);
            flash.GetComponent<SpawnOnClick>().Spawn();
        }
        else
        {
            //  flash.GetComponent<SpriteRenderer>().enabled = false;
            flash.SetActive(false);
        }
    }

}
