using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerSkills
{
    tiroNormal,
    tiroTriplo,
    seila,
    etc
}

public class PlatformerController : MonoBehaviour
{
    public PlayerSkills skillAtual = PlayerSkills.tiroNormal;
    public bool invulneravel = false;
    public float tempoInvMax, tempoInvAtual;
    
    //vari�veis p�blicas aparecem no inspetor
    public int velocidade;
    public int forcaPulo;
    public SpriteRenderer visual;
    public bool noAr;

    public bool olhandoPraDireita = true;


    public int balaPerSec = 10;
    public float tempoAtual = 0;
    public bool tiroContinuo;
    
    // Start is called before the first frame update
    void Start()
    {
        //preparando cooldown
        tempoInvAtual = tempoInvMax;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        noAr = false;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        noAr = true;
    }





    // Update is called once per frame
    void Update()
    {

         barra.value = Mathf.Lerp(barra.value, vidaAtual, 5 * Time.deltaTime);

        
        //pegando o input horizontal

        float x = Input.GetAxis("Horizontal");
        Vector3 inputTeclado = new Vector3(x, 0, 0);

        transform.position += inputTeclado * velocidade * Time.deltaTime;
        
        //input de pulo
        if (Input.GetKeyDown(KeyCode.Space) && !noAr)
        {

            GetComponent<Rigidbody2D>().AddForce(Vector2.up * forcaPulo,ForceMode2D.Impulse);
        }

        //olhar pra esquerda ou direita
        if (x > 0)
        {
            //ta indo pra direita, flip � falso
            visual.flipX = false;
            olhandoPraDireita = true;
        }
        else if (x < 0)
        {
            //ta indo pra esquerda, flip � verdadeiro
            visual.flipX = true;
            olhandoPraDireita = false;
        }
        
        
        
        
        //atirar

        if (tiroContinuo)
        {
            TiroContinuo();
        }
        else
        {
            TiroSimples();
        }
        
        //dash

        if (Input.GetKeyDown(KeyCode.X) && !invulneravel)
        {
            Vector2 direcao = olhandoPraDireita ? Vector2.right : Vector2.left;
            GetComponent<Rigidbody2D>().AddForce(direcao * forcaPulo,ForceMode2D.Impulse);
            invulneravel = true;
            Debug.Log("invulnerável");
        }

        //cooldown da invulnerabilidade
        if (invulneravel)
        {
            tempoInvAtual -= Time.deltaTime;
            if (tempoInvAtual <= 0)
            {
                invulneravel = false;
                tempoInvAtual = tempoInvMax;
            }
        }

        
    }

    void TiroSimples()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            //cooldown pra disparar x balas por segundo
            
                //executando tiro
                if (skillAtual == PlayerSkills.tiroNormal)
                {
                    float angulo = olhandoPraDireita ? 0 : 180;
                    GetComponent<Atirador>().Atirar().Rotate(Vector3.forward*angulo);
                    GetComponent<CinemachineImpulseSource>().GenerateImpulse(0.1f);
                    GetComponent<AudioSource>().Play();
                }
                if (skillAtual == PlayerSkills.tiroTriplo)
                {
                    float angulo = olhandoPraDireita ? 0 : 180;
                    GetComponent<Atirador>().Atirar().Rotate(Vector3.forward*angulo);
                    GetComponent<Atirador>().Atirar().Rotate(Vector3.forward* (angulo + 15));
                    GetComponent<Atirador>().Atirar().Rotate(Vector3.forward*(angulo -15));
                    GetComponent<CinemachineImpulseSource>().GenerateImpulse(0.1f);
                    GetComponent<AudioSource>().Play();
                
                }
        }
    }

    void TiroContinuo()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            //cooldown pra disparar x balas por segundo
            tempoAtual -= Time.deltaTime;

            if (tempoAtual <= 0)
            {
                //executando tiro
                if (skillAtual == PlayerSkills.tiroNormal)
                {
                    float angulo = olhandoPraDireita ? 0 : 180;
                    GetComponent<Atirador>().Atirar().Rotate(Vector3.forward*angulo);
                    GetComponent<CinemachineImpulseSource>().GenerateImpulse(0.1f);
                    GetComponent<AudioSource>().Play();
                }
                if (skillAtual == PlayerSkills.tiroTriplo)
                {
                    float angulo = olhandoPraDireita ? 0 : 180;
                    GetComponent<Atirador>().Atirar().Rotate(Vector3.forward*angulo);
                    GetComponent<Atirador>().Atirar().Rotate(Vector3.forward* (angulo + 15));
                    GetComponent<Atirador>().Atirar().Rotate(Vector3.forward*(angulo -15));
                    GetComponent<CinemachineImpulseSource>().GenerateImpulse(0.1f);
                    GetComponent<AudioSource>().Play();
                }
                tempoAtual = 1.0f / balaPerSec;
            }
        }
    }
    

    public Slider barra;
    public float vidaAtual = 100;
}
