using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    public int velocidade = 20;


    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * velocidade * Time.deltaTime);
    }
}
