using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Oscilador : MonoBehaviour
{
    public TextMeshProUGUI timeTXT,fixedTimeTXT, deltaTXT, fixedTXT;//,unscaledTimeTXT, unscaledTXT;
    private float timef, fixedTimef, deltaf, fixedf;//unscaledTimef unscaledf;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timef = Time.time;
        fixedTimef = Time.fixedTime;
       // unscaledTimef = Time.unscaledTime;
        deltaf = Time.deltaTime;
        fixedf = Time.fixedDeltaTime;
       // unscaledf = Time.unscaledDeltaTime;
        AtualizarTextos();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.timeScale > 0.51f) // = 1
            {
                Time.timeScale = 0.5f;
            }
            else if (Time.timeScale is > 0.49f and < 0.51f) // = 0.5
            {
                Time.timeScale = 0;
            }
            else // 0
            {
                Time.timeScale = 1;
            }
        }
    }


    void AtualizarTextos()
    {
        timeTXT.text = timef.ToString();
        fixedTimeTXT.text = fixedTimef.ToString();
        //unscaledTimeTXT.text = unscaledTimef.ToString();
        deltaTXT.text = deltaf.ToString();
        fixedTXT.text = fixedf.ToString();
       // unscaledTXT.text = unscaledf.ToString();
    }
}
