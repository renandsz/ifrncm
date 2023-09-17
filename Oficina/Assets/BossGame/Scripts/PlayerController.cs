using System;
using Unity.VisualScripting;
using UnityEngine;

namespace BossGame.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        public int velocidade, forcaPulo, forcaDash;

        public bool movendo, noAr, olhandoDireita,naParede;

        private float h; //input horizontal
        
        private Animator animator;
        private SpriteRenderer renderer;
        private Rigidbody2D rb;
        
        
        //ponto de origem dos raycasts pra escanear as colisoes
        private Vector2 _botomLeft = new Vector2(-0.49f,-0.49f);
        private Vector2 _bottomRight = new Vector2(0.49f,-0.49f);
        private Vector2 _topLeft = new Vector2(-0.49f,0.49f);
        private Vector2 _topRight = new Vector2(0.49f,0.49f);
        private float raySize = 5; //tamanho do raio
        private BoxCollider2D playerCollider;
        private int anguloRampa = 175;
        
       
        private void Start()
        {
            //passando a referencia pro animator e spriterenderer
            //que ta na primeira child
            Transform child = transform.GetChild(0);
            child.TryGetComponent(out animator);
            child.TryGetComponent(out renderer);
        
            //pegando ref para o rigidbody2d e collider
            TryGetComponent(out rb);
            TryGetComponent(out playerCollider);
        }
 

        private void Update()
        {
            h = Input.GetAxis("Horizontal");
            movendo = h != 0; //se o input é diferente de zero, tá se movendo
            if (h > 0) //olhando pra direita
            {
                olhandoDireita = true;
                renderer.flipX = false;
                //a sprite que to usando ja olha pra direita
                //só precisa espelhar (flipar) se for olhar pra esquerda
            }
            if (h < 0)//olhando pra esquerda
            {
                olhandoDireita = false;
                renderer.flipX = true;
            }

            //raycasts pra verificar se ta na parede ou no chao
            EscaneandoTerreno();
            //movendo boneco
            Movendo();
            //pulo
            Pulo();
            AtualizarAnimator(); //manter sempre no final do update
        } 
        void Movendo()
        {
            if (h != 0 && !naParede)
            {
                transform.position += new Vector3(h,0,0) * (velocidade * Time.deltaTime);
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
                    rb.AddForce(Vector2.up * forcaPulo,ForceMode2D.Impulse);
                    noAr = true;
                }
            }
        }

        void EscaneandoTerreno()
        {
            var tp = (Vector2)transform.position;
            Vector2 dir = olhandoDireita ? Vector2.right : Vector2.left;
            Vector2 bot = olhandoDireita ? _bottomRight : _botomLeft;
            Vector2 top = olhandoDireita ? _topRight : _topLeft;
            //raycasts horizontais esq e dir
            /* esq       dir
               ---[    ]---
               ___[    ]___
            */
            RaycastHit2D hitH1 = Physics2D.Raycast(tp + bot, dir, raySize);
            RaycastHit2D hitH2 = Physics2D.Raycast(tp + top, dir, raySize);
            DbLinha(tp + bot,dir);
            DbLinha(tp + top,dir);
            
            //raycasts para baixo 1 e 2 esq e dir
            /*
               [    ]
               [    ]
                |  |  pra baixo
            */
            RaycastHit2D hitD1 = Physics2D.Raycast(tp+_botomLeft, Vector2.down, raySize);
            RaycastHit2D hitD2 = Physics2D.Raycast(tp+_bottomRight, Vector2.down, raySize);
            DbLinha(tp+_botomLeft,Vector2.down);
            DbLinha(tp+_bottomRight,Vector2.down);
            
            //lembre de adicionar o player na layer "IGNORE RAYCAST"
             if (
                     (!hitD1.collider && !hitD2.collider)
                        ||
                     (
                         (hitD1.collider && !hitD1.collider.IsTouching(playerCollider))
                            && 
                         (hitD2.collider && !hitD2.collider.IsTouching(playerCollider))
                     )
                 )
             {
                 noAr = true; //rays bateram em nada, tudo nulo = no ar
             }
             else if 
                 ((hitD1.collider && hitD1.collider.IsTouching(playerCollider) )
                  ^ (hitD2.collider && hitD2.collider.IsTouching(playerCollider)))
             {
                 //algum raycast pegou algo, ta no chao
                 noAr = false;
                 if(hitD1.collider&& hitD1.collider.IsTouching(playerCollider)) DbHit(hitD1.point);//debug
                 if(hitD2.collider&& hitD2.collider.IsTouching(playerCollider)) DbHit(hitD2.point);//debug
             }
             else if 
                 ((hitD1.collider && hitD1.collider.IsTouching(playerCollider) )
                  && (hitD2.collider && hitD2.collider.IsTouching(playerCollider)))
             {
                //os dois pegaram e os dois estao no chao
                noAr = false;
                if(hitD1.collider) DbHit(hitD1.point);//debug
                if(hitD2.collider) DbHit(hitD2.point);//debug
            }
            
            if (!hitH1.collider && !hitH2.collider )
            {
                naParede = false; //rays bateram em nada, tudo nulo = sem parede
            }
            else if 
            ((hitH1.collider && hitH1.collider.IsTouching(playerCollider) )
             || (hitH2.collider && hitH2.collider.IsTouching(playerCollider)))
            {
                //se tiver encostado em alguma parede
                //checando inclinação pra saber se é uma parede
                bool rampa = false;
                if (hitH1.collider && hitH1.collider.IsTouching(playerCollider))
                {
                    var reflectedDirection = Vector2.Reflect(dir, hitH1.normal);
                    float angle = Vector2.Angle(dir, reflectedDirection);
                    rampa = rampa || angle <= anguloRampa;
                    Debug.Log($"angulo1: {angle}");
                }
                
                if (hitH2.collider && hitH2.collider.IsTouching(playerCollider))
                {
                    var reflectedDirection = Vector2.Reflect(dir, hitH2.normal);
                    float angle = Vector2.Angle(dir, reflectedDirection);
                    rampa = rampa || angle <= anguloRampa;
                    Debug.Log($"angulo2: {angle}");
                }
                
                naParede = true;
                if(hitH1.collider) DbHit(hitH1.point);//debug
                if(hitH2.collider) DbHit(hitH2.point);//debug
            }
        }


        void AtualizarAnimator()
        {
            //verifique os parametros la na no animator
            animator.SetBool("Moving",movendo);
            animator.SetBool("OnAir",noAr);
        }

        
        //metodos usados pra desenhar linhas na tela,
        //visivel se clicar no botao gizmo da aba de game
        private void DbLinha(Vector2 startPos, Vector2 dir)
        {
            Debug.DrawLine(startPos,startPos + dir * raySize,Color.yellow,Time.deltaTime);
        }
        private void DbHit(Vector2 startPos)
        {
            Debug.DrawLine(startPos,startPos + Vector2.one * 0.5f,Color.magenta,Time.deltaTime);
        }
        
    }
}
