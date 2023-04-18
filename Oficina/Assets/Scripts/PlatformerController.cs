using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerController : MonoBehaviour
{
    //variáveis públicas aparecem no inspetor
    public int velocidade;
    public int forcaPulo;
    public SpriteRenderer visual;
    public bool noAr;

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
            //ta indo pra direita, flip é falso
            visual.flipX = false;
        }
        else if (x < 0)
        {
            //ta indo pra direita, flip é verdadeiro
            visual.flipX = true;
        }
        
    }
}
