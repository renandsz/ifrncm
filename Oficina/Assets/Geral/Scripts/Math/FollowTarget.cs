using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform alvo;
    public float velocidade = 5;
    public Vector3 posicaoPet;
    public int alcance = 5;

    void Update()
    {
        if(Vector3.Distance(transform.position,alvo.position) > alcance)
        {
            transform.position = Vector3.Lerp(transform.position, alvo.position + posicaoPet, velocidade * Time.deltaTime);

        }



        // Vector3.Lerp(transform.position, alvo.position, velocidade * Time.deltaTime);
        //a posição atual do objeto vai mudar gradualmente para a posição do alvo
    }
   
}
