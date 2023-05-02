using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToOnClick : MonoBehaviour
{
    public Transform target;
    private Vector3 posicaoAlvo;
    public bool movendo;
    public int velocidade = 5;
    public float limite = 0.001f;
    
    private void OnMouseDown()
    {
        posicaoAlvo = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        posicaoAlvo.z = transform.position.z;
        movendo = true;
    }

    private void Update()
    {
        if (movendo)
        {
            if (Vector3.Distance(target.position, posicaoAlvo)>limite)
            {
                target.position = Vector3.MoveTowards(target.position, posicaoAlvo, velocidade * Time.deltaTime);
            }
            else
            {
                target.position = posicaoAlvo;
                movendo = false;
            }
        }
    }
}
