using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public enum PlayerState
{
    repouso, //0 
    andando, //1
    pulando, //2
    ataqueParado, //3
    ataqueAndando, //4
    ataqueNoAr, //5
    caindo, //6
    machucado, //7
    morto //8 
}


public class NewPlat : MonoBehaviour
{

    public PlayerState estadoAtual;

    public bool movendo, noAr, naParede, olhandoDireita;

    public float inputX, inputY;

    public int vida = 100;
    public int velMov = 10;
    public int forPulo = 10;

    private Rigidbody2D rigidbody;
    private Collider2D playerCollider;
    public float rayHorizontal = 1, rayVertical = 1;
    

    private void Awake()
    {
        TryGetComponent(out rigidbody);
        TryGetComponent(out playerCollider);
    }

    // Start is called before the first frame update
    void Start()
    {
        estadoAtual = PlayerState.repouso;
        
    }

    // Update is called once per frame
    void Update()
    {
        LendoInputs();
        EscaneandoTerreno();
        Movendo();
        Pulo();
        
        switch (estadoAtual)
        {
            case PlayerState.repouso:
                Repouso();
                break;
            case PlayerState.andando:
                Andando();
                break;
            case PlayerState.pulando:
                Pulando();
                break;
            case PlayerState.ataqueParado:
                AtaqueParado();
                break;
            case PlayerState.ataqueAndando:
                AtaqueAndando();
                break;
            case PlayerState.ataqueNoAr:
                AtaqueNoAr();
                break;
            case PlayerState.caindo:
                Caindo();
                break;
            case PlayerState.machucado:
                Machucado();
                break;
            case PlayerState.morto:
                Morto();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void Repouso()
    {
        throw new NotImplementedException();
    }

    private void Andando()
    {
        throw new NotImplementedException();
    }

    private void Pulando()
    {
        throw new NotImplementedException();
    }

    private void AtaqueParado()
    {
        throw new NotImplementedException();
    }

    private void AtaqueAndando()
    {
        throw new NotImplementedException();
    }

    private void AtaqueNoAr()
    {
        throw new NotImplementedException();
    }

    private void Caindo()
    {
        throw new NotImplementedException();
    }

    private void Machucado()
    {
        throw new NotImplementedException();
    }

    private void Morto()
    {
        throw new NotImplementedException();
    }

    void Movendo()
    {
        
        if (inputX != 0 && !naParede)
        {
            transform.position += new Vector3(inputX,0,0) * (velMov * Time.deltaTime);
            movendo = true;
        }
        else
        {
            movendo = false;
        }
    }

    void Pulo()
    {
        if (!noAr)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rigidbody.AddForce(Vector2.up * forPulo,ForceMode2D.Impulse);
                noAr = true;
            }
        }
    }

    void LendoInputs()
    {
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");

        if (inputX > 0)
            olhandoDireita = true;
        if (inputX < 0)
            olhandoDireita = false;

        if (!noAr && rigidbody.velocity.y != 0)
        {
            movendo = false;
        }
    }

    void EscaneandoTerreno()
    {

        Vector2 dirH = olhandoDireita ? Vector2.right : Vector2.left;

        Vector2 originPoint = transform.position;
        
        RaycastHit2D hitH = Physics2D.Raycast(originPoint, dirH, rayHorizontal);
        RaycastHit2D hitD = Physics2D.Raycast(originPoint, Vector2.down, rayVertical);

        Debug.DrawLine(originPoint,originPoint+dirH*rayHorizontal , Color.yellow, Time.deltaTime,true);
        Debug.DrawLine(originPoint,originPoint+Vector2.down*rayVertical , Color.yellow, Time.deltaTime,true);

        if (hitD.collider != null)
        {
            Debug.Log("floor " + hitD.transform.name);
            Debug.DrawLine(hitD.point, hitD.point + Vector2.one*0.1f, Color.blue, Time.deltaTime);
            noAr = !hitD.collider.IsTouching(playerCollider);
        }
        else
        {
            noAr = true;
        }
        
        if (hitH.collider != null)
        {
            Debug.Log("wall " + hitH.transform.name);
            Debug.DrawLine(hitH.point, hitH.point + Vector2.one*0.1f, Color.blue, Time.deltaTime);
            naParede = hitH.collider.IsTouching(playerCollider);
        }
        else
        {
            naParede = false;
        }
        
    }

}
