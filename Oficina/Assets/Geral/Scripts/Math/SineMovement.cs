using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

    private SpriteRenderer renderer;

    private void Start()
    {
        TryGetComponent(out renderer);
    }

    void Update()
    {
        CalcularAngulo();
       // MarcarVolta();

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

    void MarcarVolta()
    {
        if (angulo is < 10 or > 350 )
        {
            renderer.color = Color.red;
        }
        else
        {
            renderer.color = Color.white;
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
       // ResetarRotacao();
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
    }

}
