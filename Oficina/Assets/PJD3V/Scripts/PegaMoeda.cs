using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PegaMoeda : MonoBehaviour
{
    public string tag;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(tag))
        {
            Destroy(collision.gameObject);//destruiu a moeda
            //somar e atualizar o contador

            FindObjectOfType<GameManager3>().AdicionarMoedas(1);

        }
    }
}
