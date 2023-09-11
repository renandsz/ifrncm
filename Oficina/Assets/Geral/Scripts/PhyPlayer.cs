using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhyPlayer : MonoBehaviour
{
    private Rigidbody2D rb;
    public int speedMultiplier = 5;

    public float maxVelocity = 10;
    // Start is called before the first frame update
    void Awake()
    {
        TryGetComponent(out rb);
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        rb.AddForce(Vector2.right*inputX,ForceMode2D.Force);

        if (rb.velocity.x > 0 && rb.velocity.x > maxVelocity)
        {
            rb.velocity = new Vector2(maxVelocity, rb.velocity.y);
        }
        
        if (rb.velocity.x < 0 && rb.velocity.x < -maxVelocity)
        {
            rb.velocity = new Vector2(-maxVelocity, rb.velocity.y);
        }
        
    }
}
