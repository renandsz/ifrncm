using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlatformerController : MonoBehaviour
{
    //vari�veis p�blicas aparecem no inspetor
    public int velocidade;
    public int forcaPulo;
    public SpriteRenderer visual;
    public bool noAr;

    public bool olhandoPraDireita = true;

    // Start is called before the first frame update
    void Start()
    {
        
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

        if (Input.GetKeyDown(KeyCode.Z))
        {
            float angulo = olhandoPraDireita ? 0 : 180; 
            
            GetComponent<Atirador>().Atirar().Rotate(Vector3.forward*angulo);
            
            GetComponent<CinemachineImpulseSource>().GenerateImpulse(0.1f);
            
            GetComponent<AudioSource>().Play();
        }
        
    }
}
