using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatform : MonoBehaviour
{

    public int velocidade;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");

        Vector3 inputTeclado = new Vector3(x, 0, 0);
        transform.position += inputTeclado * velocidade * Time.deltaTime;
        
    }
}
