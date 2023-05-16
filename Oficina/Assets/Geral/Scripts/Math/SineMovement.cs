using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public enum TipoMovimento
{
    rotacao,
    seno,
    cosseno,
    orbital,
    orbital2
}
public class SineMovement : MonoBehaviour
{

    public float angulo = 0;
    public int velocidade = 360;
    public TipoMovimento tipo;

    public float amplitude = 1f;

    [Range(0, 360)] public int anguloInicial;


    

    void Update()
    {
        CalcularAngulo();

        switch (tipo)
        {
            case TipoMovimento.rotacao:
                Rotacao();
                break;
            case TipoMovimento.seno:
                Seno();
                break;
            case TipoMovimento.cosseno:
                Cosseno();
                break;
            case TipoMovimento.orbital:
                Orbital();
                break;
            case TipoMovimento.orbital2:
                Orbital2();
                break;
            default:
                break;
        }

    }

    void CalcularAngulo()
    {
        angulo += velocidade * Time.deltaTime;
        
        if (angulo >= 360)
        {
            angulo -= 360;
        }
    }

    void ResetarRotacao()
    {
        transform.rotation = Quaternion.identity;//reseta a rotacao caso mude de tipo de movimento

    }

    void Rotacao()
    {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.Euler(0,0,angulo);
    }

    void Seno()
    {
       ResetarRotacao();
        
        float y = Mathf.Sin(Mathf.Deg2Rad  * angulo);
        transform.position = new Vector3(0, y * amplitude, 0);
    }
    void Cosseno()
    {
        transform.rotation = Quaternion.Euler(0,0,-90);
        float x = Mathf.Cos(Mathf.Deg2Rad * angulo);
        transform.position = new Vector3(x * amplitude,0, 0);
    }
    void Orbital()
    {
        ResetarRotacao();
        
        float x = Mathf.Cos(Mathf.Deg2Rad * angulo);
        float y = Mathf.Sin(Mathf.Deg2Rad * angulo);
        transform.position = new Vector3(x * amplitude, y * amplitude, 0);
    }
    void Orbital2()
    {
        Rotacao();
        
        float x = Mathf.Cos(Mathf.Deg2Rad * angulo);
        float y = Mathf.Sin(Mathf.Deg2Rad * angulo);
        transform.position = new Vector3(x * amplitude, y * amplitude, 0);




        int dado = Random.Range(0, 7); // aleatório entre 0 e 6
        
        float posicaoX = Random.Range(0.0f, 10.0f); // aleatório entre 0.0 e 10.0
        float posicaoY = Random.Range(0.0f, 10.0f); // aleatório entre 0.0 e 10.0

        Vector2 posicaoAleatória = new Vector2(posicaoX, posicaoY);
        // vector 2 aleatório



        float valorSorteado = Random.value;
        
        if (valorSorteado < 0.3f)
        {
            //30% de chance
        }
        else if (valorSorteado < 0.4f)
        {
            //10% de chance
        }
        else
        {
            //60% de chance
            //Vector3.Lerp()
          
        }
            


    }

}
