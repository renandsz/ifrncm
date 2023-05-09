using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    public GameObject bala, efeito;

    public Transform Atirar()
    {
        GameObject b =  Instantiate(bala, transform.position, Quaternion.identity);
        Instantiate(efeito, transform.position, Quaternion.identity);


        return b.transform;
    }


}
