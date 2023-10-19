using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Slider playerHP, bossHP;

    private void Awake()
    {
        instance = this;
    }

    public void InicializarPlayerHP(int vidaMax)
    {
        playerHP.maxValue = vidaMax;
    }
    public void InicializarBossHP(int vidaMax)
    {
        bossHP.maxValue = vidaMax;
    }
}
