using Unity.VisualScripting;
using UnityEngine;

namespace Geral.Scripts.Test
{
    public class Player : MonoBehaviour
    {

        private Rigidbody2D rb;
        private Vector2 inputTeclado = Vector2.zero;

        public int velocidade = 5;

        public int forcaPulo = 10;

        private bool pulo;

        private float inputX = 0f;
        // Start is called before the first frame update
        void Start()
        {
            TryGetComponent(out rb);
        }

        void Update()
        {
            inputX = Input.GetAxisRaw("Horizontal");
            //  float inputY = Input.GetAxisRaw("Vertical");

            inputTeclado = new Vector2(inputX,0);
            if(Input.GetKeyDown(KeyCode.Space))
                rb.AddForce(Vector2.up * forcaPulo,ForceMode2D.Impulse);

        }
        
        void FixedUpdate()
        {



            rb.velocity = new Vector2(rb.velocity.x + inputX * velocidade * Time.fixedDeltaTime, rb.velocity.y);
            
           // transform.position += inputTeclado * velocidade * Time.fixedDeltaTime;

           

        }
    }
}
