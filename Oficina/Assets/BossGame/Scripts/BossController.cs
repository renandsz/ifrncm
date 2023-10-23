using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

namespace BossGame.Scripts
{
    public enum Boss1State
    {
        VaiEVolta,
        Atacando,
        Intro,
        Iniciando,
        Parado,
    }

    public class BossController : MonoBehaviour
    {
        public Boss1State comportamento = Boss1State.VaiEVolta;
        public int velocidade, forcaEmpurrao;

        public bool movendo, noAr, olhandoDireita, naParede;

        public GameObject tiroPrefab;

        private float h; //input horizontal
        private Animator animator;
        private SpriteRenderer renderer;
        private Rigidbody2D rb;
        private bool deuTiro;
        public Transform player;

        private float b = 0.49f;

        private float scaleFactor = 3;
        //ponto de origem dos raycasts pra escanear as colisoes
        private Vector2 _botomLeft = new Vector2(-1,-1);
        private Vector2 _bottomRight = new Vector2(1,-1);
        private Vector2 _topLeft = new Vector2(-1,1);
        private Vector2 _topRight = new Vector2(1,1);
        private float raySize = 0.3f; //tamanho do raio
        private BoxCollider2D thisCollider;
        private int anguloRampa = 175;
        

       // public float altura = 3;
       public Transform marcadorAltura;
       
       
       
       //comportamento de atirar depois de um tempo

       public int voltasMax = 10;
       public int voltasAtuais;
       public bool atropelou,atirando;
       public float tirosPorSeg = 10;
       public float cooldownTiro;
       public float arcoTiro = 15;
       public float tempoAtirando;
       public float tempoAtirandoMax = 2;

       public int vida = 100;
       
       
       //audio
       private AudioSource source;
       public AudioClip tiro, grito, hit;
       
       
       //particulas
       public GameObject explosionPrefab;
       
       public bool phase2;
        private void Awake()
        {
            _botomLeft *= b * scaleFactor;
             _bottomRight *= b * scaleFactor;
             _topLeft *= b * scaleFactor;
             _topRight *= b * scaleFactor;
        }


        private void Start()
        {
            voltasAtuais = voltasMax;
            tempoAtirando = tempoAtirandoMax;
            player = GameObject.FindWithTag("Player").transform;
            //passando a referencia pro animator e spriterenderer
            //que ta na primeira child
            Transform child = transform.GetChild(0);
            marcadorAltura = transform.GetChild(1);
            child.TryGetComponent(out animator);
            child.TryGetComponent(out renderer);
        
            //pegando ref para o rigidbody2d e collider
            TryGetComponent(out rb);
            TryGetComponent(out thisCollider);
            TryGetComponent(out source);
            
            GameManager.instance.InicializarBossHP(100);

        }

        private void OnCollisionEnter2D(Collision2D col)
        {    
            if (col.gameObject.CompareTag("Player"))
            {
                if(col.transform.position.y <= marcadorAltura.position.y)
                    atropelou = true;
                
                Empurrar(col.gameObject.GetComponent<Rigidbody2D>());
            }
        }
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Tiro"))
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
                // boss perdeu
                Explodiu();
            }

            GameManager.instance.bossHP.value = vida;

            if (!phase2 && vida <= 50)
            {
                phase2 = true;
                EntrarComportamento2();
            }
        }

        void Explodiu()
        {
            Instantiate(explosionPrefab, transform.position,quaternion.identity);
            GameManager.instance.Explodiu();
            Destroy(gameObject);
        }
        void Empurrar(Rigidbody2D playerRb)
        {
            Vector2 dir = olhandoDireita ? Vector2.right : Vector2.left;

            Vector2 arco = new Vector2(dir.x, 0.2f);
            playerRb.AddForce(arco * forcaEmpurrao,ForceMode2D.Impulse);
        }
        void InputSimulado()
        {
            if (naParede || atropelou)
            {
                h *= -1;
                source.PlayOneShot(hit);
                if(!atropelou)
                {
                    voltasAtuais--;
                    if (voltasAtuais <= 0)
                    {
                        voltasAtuais = voltasMax;
                        comportamento = Boss1State.Atacando;
                    }
                }
            }
        }

        void InputInicial()
        {
            if (player.position.x <= transform.position.x)
            {
                h = -1;
            }else if (player.position.x > transform.position.x)
            {
                h = 1;
            }
        }

        private void Update()
        {
            
            switch (comportamento)
            {
                case Boss1State.Intro:
                    return;
                case Boss1State.Iniciando:
                    InputInicial();
                    comportamento = Boss1State.VaiEVolta;
                    break;
                case Boss1State.Parado:
                    //espera um tempo antes de atacar
                    break;
                case Boss1State.VaiEVolta:
                    InputSimulado();
                    //movendo boneco
                    ChecarLado();
                    Movendo();
                    break;
                case Boss1State.Atacando:
                    // Tiro
                    Tiro();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
           
            

            //raycasts pra verificar se ta na parede ou no chao
            EscaneandoTerreno();
            
            
            
            //animação
            AtualizarAnimator(); //manter sempre no final do update
        }

        void ChecarLado()
        {
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
        }

        private void Tiro()
        {
            atirando = true;
            tempoAtirando -= Time.deltaTime;

            cooldownTiro -= Time.deltaTime;
            if (cooldownTiro <= 0)
            {
                cooldownTiro = 1 / tirosPorSeg;
                GameObject novoTiro = Instantiate(tiroPrefab, transform.position, Quaternion.identity);
                source.PlayOneShot(tiro);
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
                atirando = false;
                comportamento = Boss1State.VaiEVolta;
            }
        }

        void Movendo()
        {
            float mag = (Math.Abs(rb.velocity.x));
            //Debug.Log(mag);
            if (h != 0 && !naParede)
            {
                rb.velocity = new Vector3(h, 0, 0) * (velocidade);
               // transform.position += new Vector3(h,0,0) * (velocidade * Time.deltaTime);
                
               movendo = true;
            }
            else if (mag <= 0.02f || atirando )
            {
                movendo = false;
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
                         (hitD1.collider && !hitD1.collider.IsTouching(thisCollider))
                            && 
                         (hitD2.collider && !hitD2.collider.IsTouching(thisCollider))
                     )
                 )
             {
                 noAr = true; //rays bateram em nada, tudo nulo = no ar
             }
             else if 
                 ((hitD1.collider && hitD1.collider.IsTouching(thisCollider) )
                  ^ (hitD2.collider && hitD2.collider.IsTouching(thisCollider)))
             {
                 //algum raycast pegou algo, ta no chao
                 noAr = false;
                 if(hitD1.collider&& hitD1.collider.IsTouching(thisCollider)) DbHit(hitD1.point);//debug
                 if(hitD2.collider&& hitD2.collider.IsTouching(thisCollider)) DbHit(hitD2.point);//debug
             }
             else if 
                 ((hitD1.collider && hitD1.collider.IsTouching(thisCollider) )
                  && (hitD2.collider && hitD2.collider.IsTouching(thisCollider)))
             {
                //os dois pegaram e os dois estao no chao
                noAr = false;
                if(hitD1.collider) DbHit(hitD1.point);//debug
                if(hitD2.collider) DbHit(hitD2.point);//debug
            }
            
            if (!hitH1.collider && !hitH2.collider )
            {
                naParede = false; //rays bateram em nada, tudo nulo = sem parede
                atropelou = false;
            }
            else if 
            ((hitH1.collider && hitH1.collider.IsTouching(thisCollider) )
             || (hitH2.collider && hitH2.collider.IsTouching(thisCollider)))
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
            animator.SetBool("Shooting",atirando);
            
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
        
        public void TerminouIntro()
        {
            comportamento = Boss1State.Iniciando;
        }

        public void Grito()
        {
            source.PlayOneShot(grito);
        }

        
        public void EntrarComportamento2()
        {
            tirosPorSeg *= 1.5f;
            velocidade *= 2;
            arcoTiro /= 2;
            animator.GetComponent<SpriteRenderer>().color = Color.red;
            Grito();
        }
        
    }
    
}
