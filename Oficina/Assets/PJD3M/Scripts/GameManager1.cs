using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager1 : MonoBehaviour
{

    public TextMeshProUGUI mensagem;
    public int delay;

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


    // Update is called once per frame
    void Update()
    {
        
    }
}
