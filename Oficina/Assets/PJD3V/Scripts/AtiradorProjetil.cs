using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtiradorProjetil : MonoBehaviour
{

    public GameObject projetil, efeito;

    public Transform  Atirar()
    {
        GameObject p =  Instantiate(projetil, transform.position, Quaternion.identity);
        Instantiate(efeito, transform.position, Quaternion.identity);

        return p.transform;
    
    }

}
