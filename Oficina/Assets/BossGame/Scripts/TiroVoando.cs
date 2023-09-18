using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiroVoando : MonoBehaviour
{
    public int velocidade = 10;

    public float tempoDeVida = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Cenario"))
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        transform.Translate(Vector3.right * velocidade * Time.deltaTime);
        tempoDeVida -= Time.deltaTime;
        if (tempoDeVida <= 0) Destroy(gameObject);
    }
}
