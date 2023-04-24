using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownController : MonoBehaviour
{
    public int velocidade;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //pegando o input do teclado

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        //Debug.Log("Horizontal: " + x);
        //Debug.Log("Vertical: " + y);

        Vector3 inputTeclado = new Vector3(x, y, 0); //criou um vetor com os valores de input

        //usando o input para mover o transform

        transform.position += inputTeclado * velocidade * Time.deltaTime;

    }
}
