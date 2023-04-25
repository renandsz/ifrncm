using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coletar : MonoBehaviour
{
    public string tag;

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(tag))
        {
            Destroy(collision.gameObject);
            FindObjectOfType<GameManager1>().AdicionarMoedas(1);
        }
    }
}
