using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moeda : MonoBehaviour
{
    public int velocidadeGiro = 25;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            FindObjectOfType<GameManager>().SomarPonto(-1);
            Destroy(gameObject);
        }
    }


    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, velocidadeGiro * Time.deltaTime,Space.World);
    }
}
