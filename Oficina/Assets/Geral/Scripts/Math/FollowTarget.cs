using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform alvo;
    public float velocidade = 5;
    void Update()
    {
        transform.position =
            Vector3.Lerp(transform.position, alvo.position, velocidade * Time.deltaTime);
        //a posição atual do objeto vai mudar gradualmente para a posição do alvo
    }
}
