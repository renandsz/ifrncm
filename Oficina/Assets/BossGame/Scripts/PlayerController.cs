using System;
using UnityEngine;

namespace BossGame.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        public int velocidade, forcaPulo, forcaDash;

        public bool movendo, noAr, olhandoPraDireita;

        private Animator animator;
        private SpriteRenderer renderer;
        private Rigidbody2D rb;
        
       // private Vector2
        
       
        private void Start()
        {
            //passando a referencia pro animator e spriterenderer
            //que ta na primeira child
            Transform child = transform.GetChild(0);
            child.TryGetComponent(out animator);
            child.TryGetComponent(out renderer);
        
            //pegando ref para o rigidbody2d
            TryGetComponent(out rb);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            
            
            RaycastHit2D hit1 = Physics2D.Raycast(transform.position, Vector2.down);
            if (hit1.collider == other)
            {
                noAr = false;
            }
        }


        private void Update()
        {
            float h = Input.GetAxis("Horizontal");
            movendo = h != 0; //se o input é diferente de zero, tá se movendo
            if (h > 0) //olhando pra direita
            {
                olhandoPraDireita = true;
                renderer.flipX = false;
                //a sprite que to usando ja olha pra direita
                //só precisa espelhar (flipar) se for olhar pra esquerda
            }
            if (h < 0)//olhando pra esquerda
            {
                olhandoPraDireita = false;
                renderer.flipX = true;
            }

            
            //movendo boneco
            Vector2 moveVector = new Vector2(h * velocidade,0);
            rb.velocity = new Vector2(moveVector.x,rb.velocity.y);
            
            
            //pulo
            if (Input.GetKeyDown(KeyCode.Space) && !noAr)
            {
                Vector2 jumpForce = Vector2.up * forcaPulo;
                rb.AddForce(jumpForce,ForceMode2D.Impulse);
                noAr = false; //sai do chao
            }
            
            AtualizarAnimator(); //manter sempre no final do update
        }

        void AtualizarAnimator()
        {
            animator.SetBool("Movendo",movendo);
            animator.SetBool("NoAr",noAr);
        }
        
    }
}
