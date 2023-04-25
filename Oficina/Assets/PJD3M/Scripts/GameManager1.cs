using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager1 : MonoBehaviour
{

    public TextMeshProUGUI mensagem, textoContador;
    public int delay;
    public int contador;

    // Start is called before the first frame update
    void Start()
    {
        mensagem.text = "Vai!";
        Invoke("MudarMensagem", delay);
    }

    void MudarMensagem()
    {
        mensagem.text = "";
    }

    public void AdicionarMoedas(int valor)
    {
        contador += valor;
        //atualizar o texto
        textoContador.text = "Moedas: " + contador;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
