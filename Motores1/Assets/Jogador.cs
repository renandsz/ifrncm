using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jogador : MonoBehaviour
{
    public int velocidade = 10;
    public Rigidbody rb;    
    void Start(){
        TryGetComponent(out rb);
    }
    void Update() {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        rb.AddForce(new Vector3(h,0,v) * velocidade);
    }
}
