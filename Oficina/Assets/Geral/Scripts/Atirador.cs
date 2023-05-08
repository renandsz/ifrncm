using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atirador : MonoBehaviour
{
    public GameObject bala,efeito;
    public Vector3 offset;
    public float lifetime = 0.5f;


    public Transform Atirar()
    {
       GameObject b =  Instantiate(bala, transform.position + offset,Quaternion.identity);
       GameObject x = Instantiate(efeito, transform.position + offset,Quaternion.identity);

       Destroy(b,lifetime);
       Destroy(x,lifetime);
       
       
       return b.transform;
    }


}
