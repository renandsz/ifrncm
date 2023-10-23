using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BossGame.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        public bool desbloqueouEspecial;
        public int velocidade, forcaPulo, forcaDash;

        public bool movendo, noAr, olhandoDireita,naParede,tomouDano;
        public float tempoMachucado = 1;
        private float tempoAtual = 0;
        public GameObject tiroPrefab;
        
        private float h; //input horizontal
        private Animator animator;
        private SpriteRenderer renderer;
        private Rigidbody2D rb;
        private bool deuTiro;

        public int vida = 100;
        

        //ponto de origem dos raycasts pra escanear as colisoes
        private Vector2 _botomLeft = new Vector2(-0.49f,-0.49f);
        private Vector2 _bottomRight = new Vector2(0.49f,-0.49f);
        private Vector2 _topLeft = new Vector2(-0.49f,0.49f);
        private Vector2 _topRight = new Vector2(0.49f,0.49f);
        private float raySize = 0.3f; //tamanho do raio
        private BoxCollider2D playerCollider;
        private int anguloRampa = 175;
        
        
        // audio
        private AudioSource source;
        public AudioClip tiro, pulo, hit, especialClip;

        public GameObject especialPrefab;
        public bool tiroEspecial;
        public float tempoAtirandoMax = 2;
        private float tempoAtirando;
        public float cooldownTiro;
        public float tirosPorSeg = 10;
        public int arcoTiro = 10;
        

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
            TryGetComponent(out source);

            tempoAtual = tempoMachucado;
            GameManager.instance.InicializarPlayerHP(100);
            tempoAtirando = tempoAtirandoMax;
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Boss"))
            {
                tomouDano = true;
                source.PlayOneShot(hit);
                SubtrairVida(5);
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("TiroBoss"))
            {
                source.PlayOneShot(hit);
                Destroy(col.gameObject);
                int valor = col.GetComponent<TiroVoando>().dano;
                SubtrairVida(valor);
            }
        }

        public void SubtrairVida(int valor)
        {
            vida -= valor;
            if (vida <= 0)
            {
                vida = 0;
                // perdeu
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            GameManager.instance.playerHP.value = vida;
        }


        private void Update()
        {
            
            animator.SetBool("Hit",tomouDano);
            if (tomouDano)
            {
                tempoAtual -= Time.deltaTime;
                if (tempoAtual <= 0)
                {
                    tomouDano = false;
                    tempoAtual = tempoMachucado;
                }
                return;
            }
        
        
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
            // Tiro
            Tiro();
            if(desbloqueouEspecial)
            {
                TiroEspecial();
            }
            //animação
            AtualizarAnimator(); //manter sempre no final do update
        }

        private void Tiro()
        {
            if(tiroEspecial) return;
            if (Input.GetKeyDown(KeyCode.Z))
            {
                source.PlayOneShot(tiro);
                deuTiro = true;
                GameObject novoTiro = Instantiate(tiroPrefab, transform.position, Quaternion.identity);
                if (!olhandoDireita)
                {
                    novoTiro.transform.Rotate(Vector3.forward,180);
                }
            }
        }
        private void TiroEspecial()
        {
            if (Input.GetKeyDown(KeyCode.X) && !tiroEspecial)
            {
                tiroEspecial = true;
            }
            if (!tiroEspecial) return;
            
            deuTiro = true;
            tempoAtirando -= Time.deltaTime;

            cooldownTiro -= Time.deltaTime;
            if (cooldownTiro <= 0)
            {
                cooldownTiro = 1 / tirosPorSeg;
                GameObject novoTiro = Instantiate(especialPrefab, transform.position, Quaternion.identity);
                source.PlayOneShot(especialClip);
                if (!olhandoDireita)
                {
                    novoTiro.transform.Rotate(Vector3.forward,180 + UnityEngine.Random.Range(-arcoTiro,arcoTiro));
                }
                else
                {
                    novoTiro.transform.Rotate(Vector3.forward, UnityEngine.Random.Range(-arcoTiro,arcoTiro));

                }
            }

            if (tempoAtirando <= 0)
            {
                tempoAtirando = tempoAtirandoMax;
                tiroEspecial = false;
                deuTiro = false;
            }
        }

        void Movendo()
        {
            if(tiroEspecial) return;
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
            if(tiroEspecial) return;
            if (!noAr)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    rb.AddForce(Vector2.up * forcaPulo,ForceMode2D.Impulse);
                    noAr = true;
                    source.PlayOneShot(pulo);
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
            animator.SetBool("Hit",tomouDano);
            if (deuTiro)
            {
                animator.SetTrigger("Shoot");
                deuTiro = false;
            }
            else
            {
                animator.ResetTrigger("Shoot");
            }
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
