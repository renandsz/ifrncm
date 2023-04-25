using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameManager2 : MonoBehaviour
{
    public TextMeshProUGUI mensagem, textoContador;
    public int contador;

    // Start is called before the first frame update
    void Start()
    {
        mensagem.text = "Vai!";
        Invoke("ModificarMensagem", 3);
    }


    void ModificarMensagem()
    {
        mensagem.text = "";
    }

    public void AdicionarMoedas(int valor)
    {
        contador += valor;
        //atualizar o texto
        textoContador.text = "Moedas: " + contador;
    }
}
