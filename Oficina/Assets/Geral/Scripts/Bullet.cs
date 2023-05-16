using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int velocidade;

    public TimeType tipo;

   public void DefinirOrientacao(float angulo)
    {
        transform.rotation = Quaternion.Euler(0,0,angulo);
    }
    
    
    
    // Update is called once per frame
    void Update()
    {
        if(tipo == TimeType.fixedT) return;
        
        transform.Translate(Vector3.right* velocidade * Time.deltaTime);
    }
    void FixedUpdate()
    {
        if(tipo == TimeType.normalT) return;
        transform.Translate(Vector3.right* velocidade * Time.fixedDeltaTime);
    }
}
