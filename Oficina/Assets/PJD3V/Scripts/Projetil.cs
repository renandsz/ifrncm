using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projetil : MonoBehaviour
{

    public int velocidade = 1;
   
    void Update()
    {
        transform.Translate(Vector2.right * velocidade * Time.deltaTime);
    }
}
