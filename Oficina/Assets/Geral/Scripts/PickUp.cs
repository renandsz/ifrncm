using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public string tag;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(tag))
        {
            Destroy(col.gameObject);
            Debug.Log("Destruiu " + col.name);
            Manager.instance.AdicionarMoedas(1);
            
        }
    }
}
