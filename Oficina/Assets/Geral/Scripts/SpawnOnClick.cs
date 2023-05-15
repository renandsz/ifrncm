using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnOnClick : MonoBehaviour
{
    public GameObject coisa;
    public bool shoot;
    public float lifetime = 0.25f;
    public void Spawn()
    {
        GameObject b = Instantiate(coisa, transform.position ,transform.rotation );
        Destroy(b, lifetime);
    }

    private void OnMouseDown()
    {
        Vector3 posicaoAlvo = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        posicaoAlvo.z = transform.position.z;
        
        Instantiate(coisa, posicaoAlvo ,Quaternion.identity );
    }

    void FixedUpdate()
    {
        if (shoot)
        {
            Spawn();
        }
    }
}
