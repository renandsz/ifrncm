using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Vector3 offset;
    public Transform player;
    public int suavidade = 5;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        offset = player.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        var origPos = transform.position;
        var newPos = player.position - offset;
        
        transform.position = Vector3.Lerp(origPos, newPos, suavidade * Time.deltaTime);
    }
}
