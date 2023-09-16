using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI hud,vitoria;
    public int moedasRestantes;
    public string proxFase;
    public AudioClip somMoeda, somVitoria;
    private AudioSource source;
    
    // Start is called before the first frame update
    void Start()
    {
        moedasRestantes = GameObject.FindGameObjectsWithTag("Moeda").Length;

        hud.text = $"Moedas restantes : {moedasRestantes}";

        TryGetComponent(out source);
    }

    public void SomarPonto(int valor)
    {
        moedasRestantes += valor;
        hud.text = $"Moedas restantes : {moedasRestantes}";
        source.PlayOneShot(somMoeda);
        
        if (moedasRestantes <= 0)
        {
            vitoria.text = "Parabéns!";
            source.PlayOneShot(somVitoria);
            Invoke(nameof(CarregarProxFase),2);
        }
    }

    
    
    void CarregarProxFase()
    {
        SceneManager.LoadScene(proxFase);
    }
    
}
