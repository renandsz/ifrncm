using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int velocidade;


    public void DefinirOrientacao(float angulo)
    {
        transform.rotation = Quaternion.Euler(0,0,angulo);
    }
    
    
    
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right* velocidade * Time.deltaTime);
    }
}
