using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnOnClick : MonoBehaviour
{
    public GameObject coisa;

    private void OnMouseDown()
    {
        Vector3 posicaoAlvo = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        posicaoAlvo.z = transform.position.z;
        
        Instantiate(coisa, posicaoAlvo ,Quaternion.identity );
    }
}
