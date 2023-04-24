using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Manager : MonoBehaviour
{
    public static Manager instance;
    public TextMeshProUGUI mensagem, contador;
    public int moedasAtuais = 0;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        mensagem.text = "Vai!";
        Invoke("LimparMensagem",5);
    }

    void LimparMensagem()
    {
        mensagem.text = "";
    }

    public void AdicionarMoedas(int quantidade)
    {
        moedasAtuais += quantidade;

        contador.text = "Moedas: " + moedasAtuais;
    }
}
