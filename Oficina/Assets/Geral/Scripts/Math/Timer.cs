using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{

    public TextMeshProUGUI timet, deltat,cooldownT;

    public float maxCooldown = 3;

    private float currentTime;

    public bool contando;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = maxCooldown;
        cooldownT.text = maxCooldown.ToString();
    }

    public float tempo = 0;
    void Update()
    {
        // Tempo começa com o valor 0
        //tempo = tempo + Time.deltaTime;
        // mesma coisa que:
        // tempo += Time.deltaTime;
        
        //deltat.text = Time.deltaTime + " segundos";

        
        if (contando) //se estamos contando o tempo
        {
            currentTime -= Time.deltaTime; //subtraia deltatime do tempo atual
            
            if (currentTime <= 0) //se tempo atual for menor ou igual a 0, o tempo acabou
            {
                currentTime = 0;
                contando = false;
            }

        }
        
        
        cooldownT.text = currentTime + " s";
        deltat.text = Time.deltaTime + " s";
        
    }

    public void SetContando(bool value)
    {
        contando = value;
    }
    
    
}
