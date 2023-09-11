using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int velocidade = 10;
    private Rigidbody rb;
    void Start() {
        TryGetComponent(out rb);
    }
    void Update() {
        float inputHorizontal = Input.GetAxis("Horizontal");
        float inputVertical = Input.GetAxis("Vertical");
        rb.AddForce(new Vector3(inputHorizontal,0,inputVertical) * velocidade);

    }
}
