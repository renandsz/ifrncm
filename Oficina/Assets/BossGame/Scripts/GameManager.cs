using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Slider playerHP, bossHP;

    private AudioSource source;
    public AudioClip explosion;
    public string proxFase;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        InicializarBossHP(100);
        InicializarPlayerHP(100);
        TryGetComponent(out source);
    }

    public void InicializarPlayerHP(int vidaMax)
    {
        playerHP.maxValue = vidaMax;
        playerHP.value = vidaMax;
    }
    public void InicializarBossHP(int vidaMax)
    {
        bossHP.maxValue = vidaMax;
        bossHP.value = vidaMax;
    }

    public void Explodiu()
    {
        source.Stop();
        source.PlayOneShot(explosion);
        Invoke(nameof(CarregarProxFase),3);
    }

    void CarregarProxFase()
    {
        SceneManager.LoadScene(proxFase);
    }
}
